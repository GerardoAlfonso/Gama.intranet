using System;
using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class ParametrosGenerales
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public int? UserCreation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
