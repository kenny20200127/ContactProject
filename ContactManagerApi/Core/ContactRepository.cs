using ContactManagerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApi.Core
{
    public class ContactRepository:Repository<Contact>,IContactRepository
    {
        private readonly ApplicationDbContext context;
        public ContactRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<Contact> GetContactByCode(Expression<Func<Contact, bool>> predicate)
        {
            return await context.Contacts.FirstOrDefaultAsync(predicate);
        }

       

       
    }
}
