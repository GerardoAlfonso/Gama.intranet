using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Response;
using Gama.Intranet.BL.Models;
using Gama.Intranet.Management;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

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

        #region CRUD_Users

        [HttpPost]
        [Route("AddUser")]
        //[ValidateAntiForgeryToken]
        public IActionResult AddUser()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.GetAllUsers();
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }catch(Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(adminDAO.getAll());
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Permissions

        [HttpPost]
        [Route("AddPermissions")]
        public IActionResult AddPermissions()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("DeletePermissions")]
        public IActionResult DeletePermissions()
        {
            throw new NotImplementedException();
        }

        #endregion

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] Usuario user)
        {
            return Ok(adminDAO.ResetPasswordUser(user.Id));
        }


        


    }
}
