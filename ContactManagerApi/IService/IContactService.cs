using ContactManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.IServices
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        void RemoveContact(Contact con);
        Task<Contact> GetContactById(int id);
        Task<bool> AddContact(Contact con);
        Task<bool> UpdateContact(Contact con);

    }
}
