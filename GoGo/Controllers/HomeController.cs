using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoGo.Models;
using Microsoft.AspNetCore.Authorization;
using GoGo.Services.Contracts;

namespace GoGo.Controllers
{
    public class HomeController : Controller
    {
        private IDestinationService destinationService;

        public HomeController(IDestinationService destinationService)
        {
            this.destinationService = destinationService;
        }

        public IActionResult Index()
        {
            var destinationModels = this.destinationService.GetDestinationsForHomePage().ToList();

            return View(destinationModels);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
