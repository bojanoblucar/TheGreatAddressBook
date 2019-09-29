using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.DataAccess.EFShared;
using AddressBook.Domain.Service;
using AddressBook.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly AddressBookDbContext context;

        private readonly IAddressBookService addressBookService;

        public PhoneNumbersController(AddressBookDbContext context, IAddressBookService addressBookService)
        {
            this.context = context;
            this.addressBookService = addressBookService;
        }

        // POST: api/PhoneNumbers
        [HttpPost]
        [Route("[action]/{id}")]
        public void ContactPhoneNumber(int id, [FromBody] PhoneNumber phoneNumber)
        {
            addressBookService.AddNewPhoneNumberToContact(id, phoneNumber);

            context.SaveChanges();
        }

        // PUT: api/PhoneNumbers/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PhoneNumber phoneNumber)
        {
            if ( id == 0 )
            {
                throw new ArgumentException("Phone number ID is missing.");
            }

            phoneNumber.Id = id;
            addressBookService.UpdatePhoneNumber(phoneNumber);

            context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            addressBookService.DeletePhoneNumber(id);

            context.SaveChanges();
        }
    }
}
