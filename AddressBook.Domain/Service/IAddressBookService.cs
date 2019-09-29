using AddressBook.Domain.Persistence;
using AddressBook.Model.Entities;
using AddressBook.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Domain.Service
{
    public interface IAddressBookService
    {
        void CreateContact(Contact contact);

        void UpdateContact(Contact contact);

        void DeleteContact(int contactId);

        Contact GetContactById(int contactId);

        PageResult<Contact> GetAllContacts(PageParams pageParams);

        void AddNewPhoneNumberToContact(int contactId, PhoneNumber phoneNumber);

        void DeletePhoneNumber(int phoneNumber);

        void UpdatePhoneNumber(PhoneNumber phoneNumber);

    }
}
