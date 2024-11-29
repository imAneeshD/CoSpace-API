using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Role");
        }
    }
}
