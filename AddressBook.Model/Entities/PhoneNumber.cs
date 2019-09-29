using AddressBook.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Model.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public PhoneNumberType Type { get; set; }

        public Contact Contact { get; set; }
    }
}
