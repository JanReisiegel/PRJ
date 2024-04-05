using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuthServerRP.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        
        public IActionResult Get()
        {
            return Ok(new JsonResult(from c in User.Claims select new { c.Type, c.Value }));
        }

    }
}
