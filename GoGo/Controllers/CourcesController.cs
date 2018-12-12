using GoGo.Models;
using GoGo.Models.Enums;
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
    public class CourcesController : Controller
    {
        private ICourcesService courcesService;
        private UserManager<GoUser> userManager;
        private SignInManager<GoUser> signInManager;

        public CourcesController(ICourcesService courcesService, UserManager<GoUser> userManager, SignInManager<GoUser> signInManager)
        {
            this.courcesService = courcesService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            var user = await userManager.GetUserAsync(HttpContext.User);

           await this.courcesService.AddCource(model, user);

            return Redirect("/Cources/All");
        }

        public IActionResult All()
        {
            var cources = this.courcesService.GetAllCources();

            return View(cources);
        }

        public async Task<IActionResult> Details(string id) // id(courceId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var cource = this.courcesService.GetDetails(id);
            if (ViewData["CurrentUser"] != null)
            {
                ViewData["CurrentUser"] = user.Id;
            }
            

            return View(cource);
        }

        public async Task<IActionResult> SignIn(string id) // id(courceId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.courcesService.AddUserToCource(id, user);

            return Redirect($"/Cources/Details/{id}");
        }

        public IActionResult AddResults(string id) // id(courceId)
        {
            var participants = this.courcesService.GetAllParticipants(id);

            return View(participants);
        }

        [HttpPost]
        public IActionResult CreateResult(UsersResultsViewModel model/*string courceId, string participantId, StatusParticitant result*/)//*StatusParticitant statusUser*/ /*UsersResultsViewModel model*/) //courceId
        {
            this.courcesService.AddResultToUsersCourses(model);
            
            return Redirect($"/Cources/AddResults/{model.CourceId}");
        }
    }
}
