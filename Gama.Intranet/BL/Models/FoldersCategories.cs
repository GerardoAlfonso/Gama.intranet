using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class FoldersCategories
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
