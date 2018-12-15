using Microsoft.AspNetCore.SignalR;
using SignalRChat.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string user, string message)
        {
            await this.Clients.All.SendAsync("NewMessage", new Message
            {
                User = user,
                Messagee = message
            });
        }
    }
}
