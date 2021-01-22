using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.Models
{
    public class AuthModel
    {
        [StringLength(50)]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
