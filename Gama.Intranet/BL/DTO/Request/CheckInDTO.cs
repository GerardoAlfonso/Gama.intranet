using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.DTO.Request
{
    public class CheckInDTO
    {
        [Required(ErrorMessage = "El usuario no puede estar vacio.")]
        public string User { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacio.")]
        public string Password { get; set; }

        public int IdUser { get; set; }



    }
}
