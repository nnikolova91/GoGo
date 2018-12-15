using GoGo.Hubs;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatService chatSerice;

        public ChatsController(IChatService chatSerice)
        {
            this.chatSerice = chatSerice;
        }

        [Authorize]
        public IActionResult Chat()
        {
            return View(chatSerice.GetAllChatUsers());
        }
    }
}
