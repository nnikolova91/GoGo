using GoGo.Models.Chat;
using System.Collections.Generic;

namespace GoGo.Services.Contracts
{
    public interface IChatService
    {
        List<ChatUser> GetAllChatUsers();
    }
}
