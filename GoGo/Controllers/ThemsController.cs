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

namespace GoGo.Controllers
{
    public class ThemsController : Controller
    {
        private IThemService themService;
        private UserManager<GoUser> userManager;

        public ThemsController(UserManager<GoUser> userManager, IThemService themService)
        {
            this.userManager = userManager;
            this.themService = themService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateThemViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.themService.AddThem(model, user);

            return Redirect($"/Thems/All");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(/*ThemCommentViewModel*/string currentComment, string themId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.themService.AddCommentToThem(themId, currentComment, user);

            
            this.TempData.Add("id", themId);

            return Redirect($"../Thems/Details/{themId}");
        }

        //[HttpPost]
        public async Task<IActionResult> Details(string themId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ThemDetailsViewModel model;

            if (themId != null)
            {
                model = this.themService.GetDetails(themId, user);
            }
            else
            {
                string themIdd = TempData["id"].ToString();
                model = this.themService.GetDetails(themIdd, user);
            }

            return View(model);
        }
        
        [Authorize]
        public IActionResult All() 
        {
            var thems = this.themService.GetAllThems();

            return View(thems);
        }
    }
}
