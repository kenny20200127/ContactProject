using AspnetIdentityApi.Models;
using AspnetIdentityCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetIdentityApi.Core
{ 
    public interface IContactManagerDbContext:IDbContext
    {
    
        DbSet<Contact> Contacts { get; set; }

    }
}
