using AddressBook.DataAccess.EFShared;
using AddressBook.Domain.Persistence;
using AddressBook.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AddressBook.DataAccess.Persistence
{
    public class PhoneNumbersDataAccess : IPhoneNumbersDataAccess
    {
        private readonly AddressBookDbContext context;

        public PhoneNumbersDataAccess(AddressBookDbContext addressBookDbContext)
        {
            this.context = addressBookDbContext;
        }

        public void Create(PhoneNumber phoneNumber)
        {
            context.PhoneNumbers.Add(phoneNumber);
        }

        public void Delete(PhoneNumber phoneNumber)
        {
            context.PhoneNumbers.Remove(phoneNumber);
        }

        public PhoneNumber GetById(int phoneNumberId)
        {
            return context.PhoneNumbers.Where(p => p.Id == phoneNumberId).FirstOrDefault();
        }
    }
}
