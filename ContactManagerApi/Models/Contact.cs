using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [StringLength(80)]
        public string Name { get; set; }
        [StringLength(14)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
    }
}
