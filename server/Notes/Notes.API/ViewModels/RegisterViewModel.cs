﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
