﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication3.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
