using AddressBook.Domain.Exceptions;
using AddressBook.Domain.Persistence;
using AddressBook.Model.Entities;
using AddressBook.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Domain.Service.Implementation
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IContactsDataAccess contactsDataAccess;

        private readonly IPhoneNumbersDataAccess phoneNumbersDataAccess;

        public AddressBookService(IContactsDataAccess contactsDataAccess, IPhoneNumbersDataAccess phoneNumbersDataAccess)
        {
            this.contactsDataAccess = contactsDataAccess;

            this.phoneNumbersDataAccess = phoneNumbersDataAccess;
        }

        public Contact GetContactById(int contactId)
        {
            return contactsDataAccess.GetById(contactId);
        }

        public PageResult<Contact> GetAllContacts(PageParams pageParams)
        {
            return contactsDataAccess.GetAll(pageParams);
        }

        public void CreateContact(Contact contact)
        {
            if ( contactsDataAccess.IsInAddressBook(contact) )
            {
                throw new DuplicateContactException();
            }

            contactsDataAccess.Create(contact);
        }

        public void UpdateContact(Contact contact)
        {
            Contact dbContact = contactsDataAccess.GetById(contact.Id);

            dbContact.Name = contact.Name;
            dbContact.DateOfBirth = contact.DateOfBirth;

            dbContact.Address.StreetName = contact.Address.StreetName;
            dbContact.Address.StreetNumber = contact.Address.StreetNumber;
            dbContact.Address.City = contact.Address.City;
            dbContact.Address.PostalCode = contact.Address.PostalCode;
            dbContact.Address.StateId = contact.Address.StateId;

            if (contactsDataAccess.IsInAddressBook(dbContact))
            {
                throw new DuplicateContactException();
            }
        }

        public void DeleteContact(int contactId)
        {
            Contact contact = contactsDataAccess.GetById(contactId);

            if ( contact == null )
            {
                throw new ArgumentException($"Contact with Id {contactId} is not available.");
            }

            contactsDataAccess.Delete(contact);
        }

        public void AddNewPhoneNumberToContact(int contactId, PhoneNumber phoneNumber)
        {
            Contact contact = contactsDataAccess.GetById(contactId);

            phoneNumber.Contact = contact ?? throw new ArgumentException($"Contact with Id {contactId} is not available.");

            phoneNumbersDataAccess.Create(phoneNumber);
        }

        public void DeletePhoneNumber(int phoneNumberId)
        {
            PhoneNumber phoneNumber = phoneNumbersDataAccess.GetById(phoneNumberId);

            if ( phoneNumber == null )
            {
                throw new ArgumentException($"Phone number with Id {phoneNumberId} is not available.");
            }

            phoneNumbersDataAccess.Delete(phoneNumber);
        }

        public void UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber dbPhoneNumber = phoneNumbersDataAccess.GetById(phoneNumber.Id);

            if ( phoneNumber == null )
            {
                throw new ArgumentException($"Phone number with Id {phoneNumber.Id} is not available.");
            }

            dbPhoneNumber.Number = phoneNumber.Number;
            dbPhoneNumber.Type = phoneNumber.Type;
        }
    }
}
