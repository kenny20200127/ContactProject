using ContactManagerApi.Core;
using ContactManagerApi.IServices;
using ContactManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public Task<Contact> GetContactByCode(string balcode)
        {
            return unitOfWork.contacts.GetContactByCode(x => x.Name == balcode);

        }
        public async Task<bool> AddContact(Contact con)
        {
            unitOfWork.contacts.Create(con);
            return await unitOfWork.Done();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return unitOfWork.contacts.All();
        }


        public Task<Contact> GetContactById(int id)
        {
            return unitOfWork.contacts.Find(id);
        }

        public async Task<bool> UpdateContact(Contact con)
        {
            unitOfWork.contacts.Update(con);
            return await unitOfWork.Done();
        }

        public void RemoveContact(Contact con)
        {
            unitOfWork.contacts.Remove(con);
            unitOfWork.Done();
        }

        
    }
}
