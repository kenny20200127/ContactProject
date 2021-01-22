using ContactManagerApi.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.Models
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
