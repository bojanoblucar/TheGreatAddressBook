using AddressBook.Model.Entities;
using AddressBook.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Domain.Persistence
{
    public interface IContactsDataAccess
    {
        void Create(Contact contact);

        PageResult<Contact> GetAll(PageParams pageParams);

        Contact GetById(int contactId);

        PageResult<Contact> SearchByName(string name, PageParams pageParams);

        PageResult<Contact> SearchByPhoneNumber(string phoneNumber, PageParams pageParams);

        void Delete(Contact contact);

        bool IsInAddressBook(Contact contact);
    }
}
