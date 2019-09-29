using AddressBook.DataAccess.EFShared;
using AddressBook.Domain.Persistence;
using AddressBook.Model.Entities;
using AddressBook.Shared.Extensions;
using AddressBook.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.DataAccess.Persistence
{
    public class ContactsDataAccess : IContactsDataAccess
    {
        private readonly AddressBookDbContext context;

        public ContactsDataAccess(AddressBookDbContext context)
        {
            this.context = context;
        }

        public void Create(Contact contact)
        {
            context.Contacts.Add(contact);
        }

        public void Delete(Contact contact)
        {
            context.Contacts.Remove(contact);
        }

        public PageResult<Contact> GetAll(PageParams pageParams)
        {
            return context.Contacts
                .IncludeReferences()
                .GetPage(pageParams);
        }

        public Contact GetById(int contactId)
        {
            return context.Contacts.Where(c => c.Id == contactId)
                               .IncludeReferences()
                               .FirstOrDefault();
        }

        public PageResult<Contact> SearchByName(string name, PageParams pageParams)
        {
            return context.Contacts.Where(c => c.Name.StartsWith(name))
                                   .IncludeReferences()
                                   .GetPage(pageParams);
        }

        public PageResult<Contact> SearchByPhoneNumber(string phoneNumber, PageParams pageParams)
        {
            return context.Contacts.Where(c => c.PhoneNumbers.Any(p => p.Number.StartsWith(phoneNumber)))
                                   .GetPage(pageParams);

        }


        public bool IsInAddressBook(Contact contact)
        {
            return context.Contacts.Any(c => c.Name.Equals(contact.Name)
                &&
                c.Address.ContactId != contact.Address.ContactId
                &&
                c.Address.StateId == contact.Address.StateId
                &&
                c.Address.StreetName.Equals(contact.Address.StreetName)
                &&
                c.Address.City.Equals(contact.Address.City)
                &&
                c.Address.StreetNumber.Equals(contact.Address.StreetNumber));
        }
    }

    static class ContactExtensions
    {
        public static IQueryable<Contact> IncludeReferences(this IQueryable<Contact> query)
        {
            return query.Include(c => c.Address)
                              //.ThenInclude(a => a.State)
                        .Include(c => c.PhoneNumbers);
        }
    }
}
