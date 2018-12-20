using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels;
using X.PagedList;

namespace GoGo.Controllers
{
    public class DestinationsController : Controller
    {
        private IDestinationService destinationService;
        private UserManager<GoUser> _userManager;

        public DestinationsController(IDestinationService destinationService, UserManager<GoUser> userManager)
        {
            this.destinationService = destinationService;
            this._userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateDestinationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            this.destinationService.AddDestination(model, user);

            return Redirect("/Destinations/All");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var dest = this.destinationService.FindDestination(id, user);
            if (dest == null)
            {
                throw new ArgumentException("You can not edit this page");
                //ViewData["MyError"] = "You can not edit this page";

                //return Redirect("/Views/Shared/MyError");  
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditDestinationViewModel model)
        {
            await this.destinationService.EditDestination(model);

            return Redirect($"/Destinations/Details/{model.Id}");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var dest = this.destinationService.FindToDeleteDestination(id, user);

            return View(dest);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id, DestViewModel model)
        {
            await this.destinationService.DeleteComments(id);

            await this.destinationService.DeleteDestinationsUsers(id);

            await this.destinationService.DeleteFromStories(id);

            await this.destinationService.DeleteDestination(id);

            return Redirect($"/Destinations/All");
        }

        public async Task<IActionResult> My()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var destinations = this.destinationService.FindMyDestinations(user);

            return View(destinations);
        }

        public IActionResult All(int? page)
        {
            var destinations = this.destinationService.GetAllDestinations();

            var nextPage = page ?? 1;
            var pageViewModels = destinations.ToPagedList(nextPage, 6);
            return View(pageViewModels);
        }

        public async Task<IActionResult> Details(string socialization, string id) // id(destinationId)
        {
            var us = User.Identity.Name;
            var user = HttpContext.User;
            var userr = await _userManager.GetUserAsync(HttpContext.User);

            var destination = this.destinationService.GetDetails(id, us);//user);

            ViewData["Message"] = "Register - if you dont or Login if you have an account";
            ViewData["Controller"] = "Destinations";

            return View(destination);
        }

        [Authorize]
        public async Task<IActionResult> HavePart(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var destUserModel = this.destinationService.AddUserToDestination(user, id);
            //var socialize = Enum.Parse<Socialization>(socialization);

            return View(destUserModel); //id(destinationId)
        }

        [HttpPost]
        public async Task<IActionResult> Socialize(string socialization, string id) //destinationId
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await this.destinationService.AddSocialization(user, id, socialization);

            //var usersForSocialization = this.destinationService.AllUsersFodSocialization(user, id, socialization);

            return Redirect($"/Destinations/Details/{id}");
        }
    }
}
