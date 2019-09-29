using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Model.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
