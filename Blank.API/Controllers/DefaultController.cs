using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blank.Server.Controllers
{
    public class DefaultController : ControllerBase
    {
        protected Guid GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var value = identity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            var userId = Guid.Parse(value);
            return userId;
        }
    }
}
