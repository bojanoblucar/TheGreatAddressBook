using AddressBook.Model.Entities;
using AddressBook.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.DataAccess.EFShared
{
    public class AddressBookModelBuilder
    {
        private readonly ModelBuilder modelBuilder;

        public AddressBookModelBuilder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Build()
        {
            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Contact>()
                .Property(c => c.Id);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);


            modelBuilder.Entity<PhoneNumber>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PhoneNumber>()
                .Property(p => p.Type)
                .HasConversion(t => t.ToString(),
                               t => (PhoneNumberType)Enum.Parse(typeof(PhoneNumberType), t));


            modelBuilder.Entity<PhoneNumber>()
                .HasOne(p => p.Contact)
                .WithMany(p => p.PhoneNumbers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.State)
                .WithMany(s => s.Addresses)
                .HasForeignKey(a => a.StateId);


            /*modelBuilder.Entity<State>()
                .HasData(
                    new State()
                    {
                        Name = "Croatia"
                    });*/
        }
    }
}
