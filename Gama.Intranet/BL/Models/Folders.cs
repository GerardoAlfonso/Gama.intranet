using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class Folders
    {
        [Key]
        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Name { get; set; }
        
    }
}
