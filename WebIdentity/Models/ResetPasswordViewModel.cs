﻿using System.ComponentModel.DataAnnotations;

namespace WebIdentity.Models
{
    public class ResetPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required, MinLength(8)]
        public string NewPassword { get; set; }
    }
}
