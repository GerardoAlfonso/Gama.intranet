﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Gama.Intranet.BL.Models
{
    public class Pages
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
