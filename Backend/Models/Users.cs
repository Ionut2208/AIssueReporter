﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Users
    {
        [Key]
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
