﻿using Microsoft.AspNetCore.SignalR;
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



//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GoGo.Hubs
//{
//    public class ChatHub : Hub
//    {
//        private static readonly ConcurrentDictionary<string, User> Users
//      = new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

//        public void Send(string message)
//        {

//            string sender = Context.User.Identity.Name;

//            // So, broadcast the sender, too.
//            //Clients.All.received(new { sender = sender, message = message, isPrivate = false });
//        }

//        public void Send(string message, string to)
//        {

//            User receiver;
//            if (Users.TryGetValue(to, out receiver))
//            {

//                User sender = GetUser(Context.User.Identity.Name);

//                IEnumerable<string> allReceivers;
//                lock (receiver.ConnectionIds)
//                {
//                    lock (sender.ConnectionIds)
//                    {

//                        allReceivers = receiver.ConnectionIds.Concat(sender.ConnectionIds);
//                    }
//                }

//                foreach (var cid in allReceivers)
//                {
//                    //Clients.Client(cid).received(new { sender = sender.Name, message = message, isPrivate = true });
//                }
//            }
//        }

//        public IEnumerable<string> GetConnectedUsers()
//        {

//            return Users.Where(x => {

//                lock (x.Value.ConnectionIds)
//                {

//                    return !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
//                }

//            }).Select(x => x.Key);
//        }

//        public override Task OnConnectedAsync()
//        {

//            string userName = Context.User.Identity.Name;
//            string connectionId = Context.ConnectionId;

//            var user = Users.GetOrAdd(userName, _ => new User
//            {
//                Name = userName,
//                ConnectionIds = new HashSet<string>()
//            });

//            lock (user.ConnectionIds)
//            {

//                user.ConnectionIds.Add(connectionId);

//                // // broadcast this to all clients other than the caller
//                // Clients.AllExcept(user.ConnectionIds.ToArray()).userConnected(userName);

//                // Or you might want to only broadcast this info if this 
//                // is the first connection of the user
//                if (user.ConnectionIds.Count == 1)
//                {

//                    Clients.Others.userConnected(userName);
//                }
//            }

//            return base.OnConnectedAsync();
//        }

//        public override Task OnDisconnectedAsync(Exception e)
//        {

//            string userName = Context.User.Identity.Name;
//            string connectionId = Context.ConnectionId;

//            User user;
//            Users.TryGetValue(userName, out user);

//            if (user != null)
//            {

//                lock (user.ConnectionIds)
//                {

//                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

//                    if (!user.ConnectionIds.Any())
//                    {

//                        User removedUser;
//                        Users.TryRemove(userName, out removedUser);

//                        // You might want to only broadcast this info if this 
//                        // is the last connection of the user and the user actual is 
//                        // now disconnected from all connections.
//                        Clients.Others.userDisconnected(userName);
//                    }
//                }
//            }

//            return base.OnDisconnectedAsync();
//        }

//        private User GetUser(string username)
//        {

//            User user;
//            Users.TryGetValue(username, out user);

//            return user;
//        }
//        //public async Task SendMessage(string user, string message) 
//        //{
//        //    //await Clients.All.SendAsync("ReceiveMessage", user, message);
//        //    await Clients.User(user).SendAsync("ReceiveMessage", message);
//        //}
//    }

//    public class User
//    {

//        public string Name { get; set; }
//        public HashSet<string> ConnectionIds { get; set; }
//    }
//}

