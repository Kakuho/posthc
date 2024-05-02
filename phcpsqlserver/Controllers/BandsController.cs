using Microsoft.AspNetCore.Mvc;

namespace Phc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        public BandsController()
        {
            // insert dependency on the dbcontext
        }

        [HttpGet("test")]
        public string TestString()
        {
            return "ayo here is some string";
        }
    }
}
