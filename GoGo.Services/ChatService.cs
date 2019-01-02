using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Chat;
using GoGo.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace GoGo.Services
{
    public class ChatService : IChatService
    {
        private IRepository<GoUser> usersRepository;

        public ChatService(IRepository<GoUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public List<ChatUser> GetAllChatUsers()
        {
            return usersRepository.All().Select(u => new ChatUser() { Id = u.Id, Username = u.UserName }).ToList();
        }
    }
}
