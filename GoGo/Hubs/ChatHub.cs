using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GoGo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            //var user = this.Context.User.Identity.Name;

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

//using GoGo.Models.Chat;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GoGo.Hubs
//{
//    [Authorize]
//    public class ChatHub : Hub
//    {
//        //public ChatHub()//services
//        //{

//        //}

//        public async Task Send( string message)
//        {
//            await this.Clients.All.SendAsync("NewMessage", new Message
//            {
//                User = this.Context.User.Identity.Name,
//                Text = message
//            });
//        }
//    }
//}
