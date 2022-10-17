using System;
using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class ContenidoHtml
    {
        [Key]
        public int Id { get; set; }
        public int IdPage { get; set; }
        public string Name { get; set; }
        public string Contenido { get; set; }
        public int UserCreation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
