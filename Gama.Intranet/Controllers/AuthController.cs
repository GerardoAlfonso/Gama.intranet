using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
using Gama.Intranet.BL.DTO.Response;
using Gama.Intranet.BL.Models;
using Gama.Intranet.Management;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

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
            dto.Password = Crypto.GetSHA256(dto.Password);
            try
            {
                Usuario result = authDAO.getUserInfo(dto.User);
                if (result != null)
                {
                    if (result.Status == 1)
                    {
                        Usuario _loggedUser = authDAO.LogIn(dto);
                        if (_loggedUser != null && _loggedUser.LoginAttempts == 0)
                        {
                            //JWT
                            logIn.Status = 1;
                            logIn.Message = "Ok";
                            logIn.Token = _loggedUser.Token;
                            logIn.ShouldChangePassword = _loggedUser.ShouldChangePassword;
                        }
                        else
                        {
                            logIn.Status = 0;
                            logIn.Message = "Datos incorrectos, revise usuario y contraseña.";
                            logIn.Token = null;
                            logIn.ShouldChangePassword = false;
                        }
                    }
                    else
                    {
                        logIn.Status = 0;
                        logIn.Message = "EL usuario se encuentra inactivo";
                        logIn.Token = null;
                        logIn.ShouldChangePassword = false;
                    }
                }
                else
                {
                    logIn.Status = 0;
                    logIn.Message = "Usuario no encontrado";
                    logIn.Token = null;
                    logIn.ShouldChangePassword = false;
                }

                return Ok(logIn);
            }
            catch (Exception ex)
            {
                logIn.Status = 0;
                logIn.Message = ex.Message.ToString();
                logIn.Token = null;
                return BadRequest(logIn);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO _user)
        {
            LogInDTO logIn = new LogInDTO();
            try
            {
                Usuario result = authDAO.getUserInfo(_user.User);
                _user.Password = Crypto.GetSHA256(_user.Password);
                if (result != null)
                {
                    if (result.Status == 1)
                    {
                        if(result.ShouldChangePassword == true)
                        {
                            if (result.Token == _user.Token)
                            {
                                Usuario user = authDAO.ChangePassword(result, _user);
                                logIn.Status = 1;
                                logIn.Message = "La contraseña se cambio con exito";
                                logIn.Token = user.Token;

                            }
                            else
                            {
                                logIn.Status = 0;
                                logIn.Message = "EL token no es valido";
                                logIn.Token = null;
                            }
                        }
                        else
                        {
                            logIn.Status = 0;
                            logIn.Message = "La operacion para el cambio de contraseña no esta disponible para este usuario, un administrador debe autorizar este proceso.";
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
            catch (Exception ex)
            {
                logIn.Status = 0;
                logIn.Message = ex.Message.ToString();
                logIn.Token = null;
                return BadRequest(logIn);
            }
        }
    }
}


        //public IActionResult GetDir()
        //{
        //    //string directorio = @"C:\Users\gerar\Desktop\Test";

        //    ////string[] ficheros = Directory.GetFiles(directorio);

        //    //string[] fileArray = Directory.GetFiles(directorio, "*.*", SearchOption.TopDirectoryOnly);

        //    return Ok(fileArray);
        //}
