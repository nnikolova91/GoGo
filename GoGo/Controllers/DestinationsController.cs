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

            return View();
        }

        public IActionResult All()
        {
            var destinations = this.destinationService.GetAllDestinations();

            return View(destinations);
        }

        public IActionResult Details(string id)
        {
            var destination = this.destinationService.GetDetails(id);            

            ViewData["Message"] = "Register - if you dont or Login if you have an account";

            return View(destination);
        }

        public async Task<IActionResult> HavePart(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            this.destinationService.AddUserToDestination(user, id);

            return Redirect($"/Destinations/Details/{id}");
        }

        [HttpPost]
        public async Task<IActionResult> Socialize(string socialization, string id) //destinationId
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            this.destinationService.Socializer(socialization, id);

            return View();
        }
    }
}
