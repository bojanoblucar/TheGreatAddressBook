using AddressBook.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Api.MessageHub
{
    public interface IAddressBookBroadcaster
    {
        Task BroadcastContactsChanged(Contact contact);
    }
}
