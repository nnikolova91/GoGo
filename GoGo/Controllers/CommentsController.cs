using GoGo.Models;
using GoGo.Services.Contracts;
using GoGo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class CommentsController : Controller
    {
        private ICommentsService commentsService;
        private UserManager<GoUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<GoUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        
        public async Task<IActionResult> AddComment(string currentComment, string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            this.commentsService.AddComment(currentComment, id, user);

            return Redirect($"/Destinations/Details/{id}");
        }
    }
}
