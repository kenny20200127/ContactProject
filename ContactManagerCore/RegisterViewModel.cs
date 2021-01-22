using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AspnetIdentityCore
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
