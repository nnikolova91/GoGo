using GoGo.Models;
using GoGo.Models.Enums;
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

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var dest = this.courcesService.FindCourse(id);

            return View(dest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            
            await this.courcesService.EditCourse(model, user);

            return Redirect($"/Cources/Details/{model.Id}");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var curse = this.courcesService.FindCourseForDelete(id);

            return View(curse);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, DeleteCourseViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.courcesService.DeleteCourse(id, user);

            return Redirect($"/Cources/All");
        }

        public IActionResult All(int? page)
        {
            var cources = this.courcesService.GetAllCources();

            var nextPage = page ?? 1;
            var pageViewModels = cources.ToPagedList(nextPage, 2);

            return View(pageViewModels);
        }

        public async Task<IActionResult> Details(int? page, string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var cource = this.courcesService.GetDetails(page, id);

            if (cource == null)
            {
                ViewData["CurrentUser"] = user.Id;
            }

            return View(cource);
        }

        public async Task<IActionResult> My()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var cources = this.courcesService.GetMyCources(user.Id);

            return View(cources);
        }

        public async Task<IActionResult> SignIn(string id) // id(courceId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.courcesService.AddUserToCource(id, user);

            return Redirect($"/Cources/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> AddResults(string id) // id(courceId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var participants = this.courcesService.GetAllParticipants(id, user);

            return View(participants);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult(UsersResultsViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.courcesService.AddResultToUsersCourses(model, user);

            return Redirect($"/Cources/AddResults/{model.CourceId}");
        }
    }
}
