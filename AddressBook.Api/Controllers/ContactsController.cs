using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Api.MessageHub;
using AddressBook.DataAccess.EFShared;
using AddressBook.Domain.Exceptions;
using AddressBook.Domain.Service;
using AddressBook.Model.Entities;
using AddressBook.Shared.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AddressBookDbContext addressBookDbContext;

        private readonly IAddressBookService addressBookService;

        private readonly IAddressBookBroadcaster addressBookBroadcaster;

        public ContactsController(AddressBookDbContext addressBookDbContext, IAddressBookService addressBookService, IAddressBookBroadcaster addressBookBroadcaster)
        {
            this.addressBookDbContext = addressBookDbContext;
            this.addressBookService = addressBookService;
            this.addressBookBroadcaster = addressBookBroadcaster;
        }


        // GET: api/Contacts
        [HttpGet]
        public PageResult<Contact> Get()
        {
            return addressBookService.GetAllContacts(new PageParams(1));
        }

        [HttpGet]
        [Route("[action]/{page}")]
        public PageResult<Contact> Page(int page)
        {
            return addressBookService.GetAllContacts(new PageParams(page));
        }

        // GET: api/Contacts/5
        [HttpGet("{id}", Name = "Get")]
        public Contact Get(int id)
        {
            return addressBookService.GetContactById(id);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task Post([FromBody] Contact contact)
        {
            //using filter to validate model state

            addressBookService.CreateContact(contact);

            addressBookDbContext.SaveChanges();

            await addressBookBroadcaster.BroadcastContactsChanged(contact);
        }

        // PUT: api/Contacts
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Contact contact)
        {
            if ( id == 0 )
            {
                throw new ArgumentException("Missing ID");
            }

            contact.Id = id;

            addressBookService.UpdateContact(contact);

            addressBookDbContext.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            addressBookService.DeleteContact(id);

            addressBookDbContext.SaveChanges();
        }
    }
}
