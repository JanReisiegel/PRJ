using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiMVCTut.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new 
                JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        //[Authorize(Policy = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdmin()
        {
            return new 
                JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
