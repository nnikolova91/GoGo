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
    public class CourcesController : Controller
    {
        private ICourcesService courcesService;
        private UserManager<GoUser> userManager;

        public CourcesController(ICourcesService courcesService, UserManager<GoUser> userManager)
        {
            this.courcesService = courcesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourceViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            this.courcesService.AddCource(model, user);

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

            ViewData["CurrentUser"] = user.Id;

            return View(cource);
        }
        
        public async Task<IActionResult> SignIn(string id) // id(courceId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            this.courcesService.AddUserToCource(id, user);

            return Redirect($"/Cources/Details/{id}");
        }

        public IActionResult AddResults(string id) // id(courceId)
        {
            var participants = this.courcesService.GetAllParticipants(id);

            return View(participants);
        }
    }
}
