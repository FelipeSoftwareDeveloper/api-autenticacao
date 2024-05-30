using Blank.Application.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blank.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar([FromBody] UserInfo userInfo)
        {
            var user = new IdentityUser
            {
                UserName = userInfo.Email,
                Email = userInfo.Email
            };

            var result = await _userManager.CreateAsync(user, userInfo.Senha);

            if (result.Succeeded)
                return Ok(new { sucesso = true, Mensagem = "Usuário criado com sucesso!" });
            else
                return BadRequest(new { sucesso = false, Mensagem = "Erro ao criar o usuário!" });

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Senha, isPersistent: false, lockoutOnFailure: false);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (result.Succeeded)
                return GenerateToken(userInfo, user.Id);
            else
                return BadRequest(new { sucesso = false, Mensagem = "Email ou senha incorreto!" });

        }

        [HttpPost("esqueci-minha-senha")]
        public async Task<ActionResult<UserToken>> EsqueciSenha([FromBody] EsqueciMinhaSenhaDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email.ToUpper());

            if (user is null)
                return BadRequest(new { sucesso = false, Mensagem = "Email incorreto!" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(new { sucesso = true, Mensagem = token });
        }

        private UserToken GenerateToken(UserInfo userInfo, string userId)
        {

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken token = new(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken() { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = expiration };
        }
    }
}
