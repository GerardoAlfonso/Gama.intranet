using System;
using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        public int IdLogStatus { get; set; }
        public string Page { get; set; }
        public string Description { get; set; }
        public int? UserCreation { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Logs(int idLogStatus, string page, string description, int? userCreation, DateTime? createdAt)
        {
            IdLogStatus = idLogStatus;
            Page = page;
            Description = description;
            UserCreation = userCreation;
            CreatedAt = createdAt;
        }

    }
}
