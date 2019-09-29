using AddressBook.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Domain.Persistence
{
    public interface IPhoneNumbersDataAccess
    {
        void Delete(PhoneNumber phoneNumber);

        void Create(PhoneNumber phoneNumber);

        PhoneNumber GetById(int phoneNumberId);
    }
}
