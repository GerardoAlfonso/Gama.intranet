using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.DTO.Response;
using Gama.Intranet.BL.Models;
using Gama.Intranet.Management;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [Route("LogIn")]
        public IActionResult LogIn([FromBody] CheckInDTO dto)
        {
            LogInDTO logIn = new LogInDTO();

            try
            {
                Usuario result = authDAO.getUserInfo(dto.User);
                if (result != null)
                {
                    if (result.Status == 1)
                    {
                        Usuario _loggedUser = authDAO.LogIn(dto);
                        if (_loggedUser != null)
                        {
                            //JWT
                            logIn.Status = 1;
                            logIn.Message = "Ok";
                            logIn.Token = Crypto.CreateJWT(_loggedUser.Name);
                        }
                        else
                        {
                            logIn.Status = 0;
                            logIn.Message = "Datos incorrectos, revise usuario y contraseña.";
                            logIn.Token = null;
                        }
                    }
                    else
                    {
                        logIn.Status = 0;
                        logIn.Message = "EL usuario se encuentra inactivo";
                        logIn.Token = null;
                    }
                }
                else
                {
                    logIn.Status = 0;
                    logIn.Message = "Usuario no encontrado";
                    logIn.Token = null;
                }

                return Ok(logIn);
            }
            catch(Exception ex)
            {
                logIn.Status = 0;
                logIn.Message = ex.Message.ToString();
                logIn.Token = null;
                return BadRequest(logIn);
            }
        }
    }
}
