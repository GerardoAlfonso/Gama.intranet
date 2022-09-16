using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.Models;
using Gama.Intranet.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Gama.Intranet.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    [Authorize(Roles = "1,0")]
    public class AdminController : ControllerBase
    {
        private readonly AdminDAO adminDAO;

        public AdminController(AdminDAO _adminDAO)
        {
            this.adminDAO = _adminDAO;
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] Usuario user)
        {
            return Ok(adminDAO.ResetPasswordUser(user.Id));
        }
    }
}
