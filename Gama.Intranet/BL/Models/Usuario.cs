using System;
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
        public string Password { get; set; }
        public int Status { get; set; }
        public int LoginAttempts { get; set; }
        public int Role { get; set; }
        public DateTime LastAccess { get; set; }
        public DateTime LastAttempDate { get; set; }
        public int UserCreation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
