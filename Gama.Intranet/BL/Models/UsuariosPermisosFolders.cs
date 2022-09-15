using System;
using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class UsuariosPermisosFolders
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdFolder { get; set; }
        public Boolean Read { get; set; }
        public Boolean Write { get; set; }

    }
}
