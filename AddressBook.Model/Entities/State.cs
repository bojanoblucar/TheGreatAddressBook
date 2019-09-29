using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Model.Entities
{
    public class State
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
