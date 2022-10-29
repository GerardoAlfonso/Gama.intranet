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
    //[Authorize(Roles = "1,0")]
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
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.getById(id);
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }


        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.GetAllUsersActive();
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
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.getAll();
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }

        [HttpPost]
        [Route("UpdateUser")]
        
        public IActionResult UpdateUser([FromBody] Usuario user)
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                Usuario result = adminDAO.getById(user.Id);
                if(result == null)
                {
                    dto.Status = 0;
                    dto.Message = "Error: no se encontro el usuario que intenta actualizar";
                    dto.Data = null;
                    return Ok(dto);
                }
                
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = adminDAO.update(result, user);
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }

        [HttpPost]
        [Route("DeleteUser")]

        public IActionResult DeleteUser([FromBody] Usuario user)
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                Usuario entity = adminDAO.getById(user.Id);
                if (entity == null)
                {
                    dto.Status = 0;
                    dto.Message = "Error: no se encontro el usuario que intenta desactivar";
                    dto.Data = null;
                    return Ok(dto);
                }
                
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = adminDAO.deleteById(entity);
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
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

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(int id)
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.ResetPasswordUser(id);
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }



        [HttpGet]
        [Route("GenerateRandomPassword")]
        public IActionResult GenerateRandomPassword()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = Crypto.RandomString(8);
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }

        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.GetRoles();
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }


        [HttpGet]
        [Route("GetStatus")]
        public IActionResult GetStatus()
        {
            GenericDTO dto = new GenericDTO();
            try
            {
                var data = adminDAO.GetStatus();
                dto.Status = 1;
                dto.Message = "Success";
                dto.Data = data;
            }
            catch (Exception ex)
            {
                dto.Status = 0;
                dto.Message = "Error: " + ex.Message;
                dto.Data = null;
            }
            return Ok(dto);
        }


    }
}
