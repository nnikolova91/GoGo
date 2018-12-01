using GoGo.Models;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class StoriesController : Controller
    {
        private IStoriesService storiesService;
        private UserManager<GoUser> userManager;

        public StoriesController(IStoriesService storiesService, UserManager<GoUser> userManager)
        {
            this.storiesService = storiesService;
            this.userManager = userManager;
        }
        
        public IActionResult Create(string id)
        {
            return View($"/Stories/Create/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateStory(string title, string content, string id)// destinationId
        {
            //if (title == null && content==null)
            //{
            //    return Redirect($"/Stories/Create/{id}");
            //}
            var user = await userManager.GetUserAsync(HttpContext.User);

            this.storiesService.AddStory(title, content, id, user);

            return Redirect($"/Destinations/Details/{id}");
        }
    }
}
