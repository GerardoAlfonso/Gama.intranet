using Gama.Intranet.BL.DAO;
using Gama.Intranet.BL.DTO.Request;
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
    //[Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AuthDAO authDAO;
        private readonly LogsDAO logsDAO;

        public AuthController(AuthDAO authDAO, LogsDAO logsDAO)
        {
            this.authDAO = authDAO;
            this.logsDAO = logsDAO;
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
                if (result == null)
                {
                    logIn.Status = 0;
                    logIn.Message = "Usuario no encontrado";
                    logIn.Token = null;
                    logIn.ShouldChangePassword = false;
                    return Ok(logIn);
                }
                if (result.Status != 1)
                {
                    logIn.Status = 0;
                    logIn.Message = "EL usuario se encuentra inactivo";
                    logIn.Token = null;
                    logIn.ShouldChangePassword = false;

                    logsDAO.WriteLog(new Logs(2, "Login", "Intento de inicio de sesion con un usuario inactivo", result.Id, DateTime.Now));
                    return Ok(logIn);
                }
                dto.IdUser = result.Id;
                Usuario _loggedUser = authDAO.LogIn(dto);

                if (_loggedUser != null && _loggedUser.LoginAttempts != 0)
                {
                    logIn.Status = 0;
                    logIn.Message = "Datos incorrectos, revise usuario y contraseña.";
                    logIn.Token = null;
                    logIn.ShouldChangePassword = false;

                    logsDAO.WriteLog(new Logs(2, "Login", "Intento de inicio de sesion con credenciales incorrectas", result.Id, DateTime.Now));


                    return Ok(logIn);
                }
                //JWT
                logIn.Status = 1;
                logIn.Message = "Ok";
                logIn.Token = _loggedUser.Token;
                logIn.UserName = _loggedUser.Name;
                logIn.IdUser = _loggedUser.Id;
                logIn.ShouldChangePassword = _loggedUser.ShouldChangePassword;

                logsDAO.WriteLog(new Logs(1, "Login", "Inicio de sesion exitoso", result.Id, DateTime.Now));
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
        [Route("LogOut")]
        [Authorize(Roles = "1,2")]
        public IActionResult LogOut([FromBody] LoggedUserDTO _loggedUser)
        {
            GenericDTO response = new GenericDTO();
            try
            {
                Usuario user = authDAO.getUserInfo(_loggedUser.User);
                if (user == null)
                {
                    response.Status = 0;
                    response.Message = "Usuario no encontrado";
                    response.Data = null;
                    return Ok(response);
                }
                if (user.Status != 1)
                {
                    response.Status = 0;
                    response.Message = "EL usuario se encuentra inactivo";
                    response.Data = null;
                    return Ok(response);
                }
                if (user.Token != _loggedUser.Token)
                {
                    response.Status = 0;
                    response.Message = "EL token no es valido";
                    response.Data = null;
                    return Ok(response);
                }
                int result = authDAO.LogOut(user);
                response.Status = 1;
                response.Message = "Se cerro la sesion con exito";
                response.Data = null;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = ex.Message.ToString();
                response.Data = null;
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(Roles = "1,2")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO _user)
        {
            LogInDTO logIn = new LogInDTO();
            try
            {
                Usuario result = authDAO.getUserInfo(_user.User);
                _user.Password = Crypto.GetSHA256(_user.Password);
                if (result == null)
                {
                    logIn.Status = 0;
                    logIn.Message = "Usuario no encontrado";
                    logIn.Token = null;
                    return Ok(logIn);
                }
                if (result.Status != 1)
                {
                    logIn.Status = 0;
                    logIn.Message = "EL usuario se encuentra inactivo";
                    logIn.Token = null;
                    return Ok(logIn);
                }
                if (result.ShouldChangePassword == false)
                {
                    logIn.Status = 0;
                    logIn.Message = "La operacion para el cambio de contraseña no esta disponible para este usuario, un administrador debe autorizar este proceso.";
                    logIn.Token = null;
                    return Ok(logIn);
                }
                if (result.Token != _user.Token)
                {
                    logIn.Status = 0;
                    logIn.Message = "EL token no es valido";
                    logIn.Token = null;
                    return Ok(logIn);
                }

                Usuario user = authDAO.ChangePassword(result, _user);
                logIn.Status = 1;
                logIn.Message = "La contraseña se cambio    con exito";
                logIn.Token = user.Token;

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

        
        [HttpGet]
        [Route("LoadHeadersAdmin")]
        [Authorize(Roles = "1")]
        public IActionResult LoadHeadersAdmin()
        {
            return Ok(1);
        }

        [HttpGet]
        [Route("LoadHeadersUser")]
        [Authorize(Roles = "2")]
        public IActionResult LoadHeadersUser()
        {
            return Ok(2);
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
