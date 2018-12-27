using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string userId, string message)
        {
            return Clients.User(userId).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
    }
}