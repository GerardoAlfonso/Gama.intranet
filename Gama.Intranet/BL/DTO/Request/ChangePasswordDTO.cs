using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.DTO.Request
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "El usuario no puede estar vacio.")]
        public string User { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacio.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Token no valido")]
        public string Token { get; set; }

        public int IdUser { get; set; }
    }
}
