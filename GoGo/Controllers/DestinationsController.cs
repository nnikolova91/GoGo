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
        public async Task<IActionResult> Create(CreateDestinationViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            this.destinationService.AddDestination(model, user);

            return Redirect("/Destinations/All");
        }

        public IActionResult All()
        {
            var destinations = this.destinationService.GetAllDestinations();

            return View(destinations);
        }

        public async Task<IActionResult> Details(string socialization, string id) // id(destinationId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var destination = this.destinationService.GetDetails(id, user);

            ViewData["Message"] = "Register - if you dont or Login if you have an account";

            return View(destination);
        }

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
