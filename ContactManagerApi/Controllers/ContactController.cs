using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerApi.IServices;
using ContactManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApi.Controllers
{
   
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService ContactService;
        public ContactController(IContactService ContactService)
        {

            this.ContactService = ContactService;
        }
        
        [Route("getAllContacts")]
        [HttpGet]
        [Authorize]
        public IEnumerable<Contact> Get()
        {
            return ContactService.GetContacts();
        }


        // GET: api/Contact/5
        [Route("Getl")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contact
        [Route("createContact")]
        [HttpPost]
        public IActionResult createContact([FromBody] Contact value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Name))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Code" });
                }
                if (ContactService.GetContactById(value.Id).Result != null)
                {
                    return Ok(new { responseCode = 400, responseDescription = "Code already Exist" });
                }
                // value.datecreated = DateTime.Now;
                ContactService.AddContact(value);

                return Ok(new { responseCode = 200, responseDescription = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

        //api/Contact/RemoveBalsheet/7
        [Route("RemoveContact/{id:int}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var balsheet = ContactService.GetContactById(id).Result;
            if (balsheet == null) return NotFound();

            ContactService.RemoveContact(balsheet);
            return Ok(new { responseCode = 200, responseDescription = "Deleted Successful" });
        }


        // PUT: api/Contact/5
        [Route("updateContact")]
        [HttpPut]
        public IActionResult Put([FromBody] Contact value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Name))
                {
                    return Ok(new { responseCode = 500, responseDescription = "Kindly Supply Cost Center Code" });
                }
                var getbal = ContactService.GetContactById(value.Id).Result;
                getbal.Name = value.Name;
                getbal.Address = value.Address;
                getbal.Email = value.Email;
                getbal.Phone = value.Phone;
                ContactService.UpdateContact(getbal);
                return Ok(new { responseCode = 200, responseDescription = "Updated Successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { responseCode = 500, responseDescription = "Failed" });
            }
        }

    }
}
