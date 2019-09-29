using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Model.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
