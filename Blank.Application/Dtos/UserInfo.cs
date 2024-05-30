using System.ComponentModel.DataAnnotations;

namespace Blank.Application.Dtos;

public class UserInfo
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }

}
