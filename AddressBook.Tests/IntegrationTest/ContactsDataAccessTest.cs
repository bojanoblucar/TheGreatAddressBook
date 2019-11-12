using AddressBook.DataAccess.EFShared;
using AddressBook.DataAccess.Persistence;
using AddressBook.Domain.Persistence;
using AddressBook.Model.Entities;
using AddressBook.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddressBook.Tests.IntegrationTest
{
    public class ContactsDataAccessTest
    {
 
        [Fact]
        public void TestCreateContact()
        {
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateContact")
                .Options;


            using(var dbContext = new AddressBookDbContext(options))
            {
                var contactsDataAccess = new ContactsDataAccess(dbContext);

                contactsDataAccess.Create(CreateNewContact());

                dbContext.SaveChanges();
            }

            using (var dbContext = new AddressBookDbContext(options))
            {
                Assert.Equal(1, dbContext.Contacts.Count());
                Assert.Equal(1, dbContext.Addresses.Count());
                Assert.Equal(1, dbContext.PhoneNumbers.Count());
                Assert.True(dbContext.Contacts.First().Id > 0);
            }
        }


        [Fact]
        public void TestDeleteContact()
        {
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
              .UseInMemoryDatabase(databaseName: "DeleteContact")
              .Options;


            Contact contact = CreateNewContact();
            using (var dbContext = new AddressBookDbContext(options))
            {
                var contactsDataAccess = new ContactsDataAccess(dbContext);
           
                contactsDataAccess.Create(contact);

                dbContext.SaveChanges();
            }

            using (var dbContext = new AddressBookDbContext(options))
            {
                var contactsDataAccess = new ContactsDataAccess(dbContext);

                Contact c = contactsDataAccess.GetById(contact.Id);
                contactsDataAccess.Delete(c);

                dbContext.SaveChanges();
            }

            using (var dbContext = new AddressBookDbContext(options))
            {
                Assert.Equal(0, dbContext.Contacts.Count());
                Assert.Equal(0, dbContext.Addresses.Count());
                Assert.Equal(0, dbContext.PhoneNumbers.Count());
            }
        }

        [Fact]
        public void TestIsInAddressBook()
        {
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateContact")
                .Options;


            using (var dbContext = new AddressBookDbContext(options))
            {
                var contactsDataAccess = new ContactsDataAccess(dbContext);

                contactsDataAccess.Create(CreateNewContact());

                dbContext.SaveChanges();
            }

            using (var dbContext = new AddressBookDbContext(options))
            {
                Assert.Equal(1, dbContext.Contacts.Count());
                Assert.Equal(1, dbContext.Addresses.Count());
                Assert.Equal(1, dbContext.PhoneNumbers.Count());
                Assert.True(dbContext.Contacts.First().Id > 0);
            }

            Contact sameContact = CreateNewContact();

            using (var dbContext = new AddressBookDbContext(options))
            {
                var contactsDataAccess = new ContactsDataAccess(dbContext);

                bool isInAddressBook = contactsDataAccess.IsInAddressBook(sameContact);

                Assert.True(isInAddressBook);

                sameContact.Name += "_Changed";

                isInAddressBook = contactsDataAccess.IsInAddressBook(sameContact);

                Assert.False(isInAddressBook);
            }
        }


        private Contact CreateNewContact()
        {
            return new Contact()
            {
                Name = "Test nakon kreiranja brancha p",
                DateOfBirth = DateTime.Today.AddDays(-1),
                Address = new Address
                {
                    StreetName = "Ilica",
                    StreetNumber = "13",
                    City = "Zagreb",
                    PostalCode = "10 000",
                    StateId = 1
                },
                PhoneNumbers = new List<PhoneNumber>()
                {
                    new PhoneNumber
                    {
                        Number = "0994223424",
                        Type = PhoneNumberType.Private
                    }
                }
            };
        }
    }
}
