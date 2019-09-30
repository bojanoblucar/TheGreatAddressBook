using AddressBook.Model.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Api.MessageHub
{
    public class AddressBookBroadcaster
    {
        private IHubContext<ContactsHub> hub;

        public AddressBookBroadcaster(IHubContext<ContactsHub> hub)
        {
            this.hub = hub;
        }

        public async Task BroadcastContactsChanged(Contact contact)
        {
            await hub.Clients.All.SendAsync("SendMessage", contact.Id, contact.Name);
        }
    }
}
