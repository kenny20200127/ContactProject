using ContactManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApi.Core 
{ 
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;

          
            contacts = new ContactRepository(context);

        }

        public IContactRepository contacts { get; set; }


        public async Task<bool> Done()
        {
           return await context.SaveChangesAsync() > 0;
        }
    }
}
