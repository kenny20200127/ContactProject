
using ContactManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerApi.Core
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactByCode(Expression<Func<Contact, bool>> predicate);
    

    }
}
