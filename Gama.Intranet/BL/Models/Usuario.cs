using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gama.Intranet.BL.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

    }
}
