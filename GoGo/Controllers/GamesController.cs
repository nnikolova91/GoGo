using GoGo.Models;
using GoGo.Services.Contracts;
using ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class GamesController : Controller
    {
        private IGamesService gamesService;
        private UserManager<GoUser> userManager;

        public GamesController(IGamesService gamesService, UserManager<GoUser> userManager)
        {
            this.gamesService = gamesService;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateGameViewModel model)
        {
            for (int i = 0; i < model.LevelsCount; i++)
            {
                model.Levels.Add(new LevelViewModel
                {
                    Image = null,
                    Description = null,
                    Points = 0
                });
            }
            var levels = model.Levels.ToList();
            this.gamesService.AddGame(model);

            return View("AddLevels", levels);

        }
        [HttpPost]
        public IActionResult AddLevels(ICollection<LevelViewModel> model)
        {
            return View(model);
        }



    }
}
