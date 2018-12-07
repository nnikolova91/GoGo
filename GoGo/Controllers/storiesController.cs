using GoGo.Models;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

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
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoryViewModel model /*string title, string content, string id*/, string id)
        {
            
            var user = await userManager.GetUserAsync(HttpContext.User);
            
            this.storiesService.AddStory(model, id, user);

            return Redirect($"/Destinations/Details/{id}");
        }

        public IActionResult Details(string id) //id(storyId)
        {
            var storyModel = this.storiesService.GetDetails(id);
            return View(storyModel);
        }

        public async Task<IActionResult> Like(string id) //id(storyId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            this.storiesService.LikeStory(id, user);

            return Redirect($"/Stories/Details/{id}");
        }
    }
}
