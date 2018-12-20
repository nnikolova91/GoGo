using GoGo.Models;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using X.PagedList;

namespace GoGo.Controllers
{
    public class StoriesController : Controller
    {
        private IStoriesService storiesService;
        private IUserStoriesService usersStoriesService;
        private UserManager<GoUser> userManager;

        public StoriesController(IStoriesService storiesService,
                                    IUserStoriesService usersStoriesService,
                                    UserManager<GoUser> userManager)
        {
            this.storiesService = storiesService;
            this.usersStoriesService = usersStoriesService;
            this.userManager = userManager;
        }
        
        [Authorize]
        public IActionResult Create(string id)
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStoryViewModel model /*string title, string content, string id*/, string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            model.DestinationId = id;

            await this.storiesService.AddStory(model, id, user);

            return Redirect($"/Destinations/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> My()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var myStories = this.storiesService.AllMyStories(user);

            return View(myStories);
        }

        public /*async Task<*/IActionResult/*>*/ All(int? page)
        {
            //var user = await userManager.GetUserAsync(HttpContext.User);
            var myStories = this.storiesService.AllStories();

            var nextPage = page ?? 1;
            var pageViewModels = myStories.ToPagedList(nextPage, 7);

            return View(pageViewModels);
        }

        public IActionResult Details(string id) //id(storyId)
        {
            var storyModel = this.storiesService.GetDetails(id);
            return View(storyModel);
        }

        [Authorize]
        public async Task<IActionResult> Like(string id) //id(storyId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.storiesService.LikeStory(id, user);

            return Redirect($"/Stories/Details/{id}");
        }
    }
}
