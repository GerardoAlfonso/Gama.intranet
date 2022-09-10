using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.GET;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Intranet.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    //[Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AuthDAO authDAO;

        public AuthController(AuthDAO authDAO)
        {
            this.authDAO = authDAO;
        }

        [HttpPost]
        [Route("CheckIn")]
        public IActionResult CheckIn([FromBody] CheckInDTO dto)
        {
            return Ok();
        }

    }
}
