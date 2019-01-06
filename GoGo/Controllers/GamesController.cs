using GoGo.Models;
using GoGo.Services.Contracts;
using ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using GoGo.Models.Enums;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = await userManager.GetUserAsync(HttpContext.User);

            string gameId = await this.gamesService.AddGame(model, user);

            return Redirect($"/Games/All");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImage(string id, string levelId, IFormFile correspondingImage) //gameId
        {
            var image = HttpContext.Request.Form.Files[0];

            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.gamesService.UserAddImageToLevel(id, user, levelId, image);

            return Redirect($"/Games/Details/{id}");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddResult(GameLevelParticipantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.gamesService.AddLevelResult(model, user);

            return Redirect($"/Games/Details/{model.GameId}");
        }

        public async Task<IActionResult> Details(string id) // id(gameId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var game = this.gamesService.GetDetails(id);

            return View(game);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Start(string id) // id(gameId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.gamesService.UserStartGame(id, user);
            
            return Redirect($"/Games/Details/{id}");
        }
        
        public IActionResult All(int? page)
        {
            var games = this.gamesService.GetAllGames().ToList();

            var nextPage = page ?? 1;
            var pageViewModels = games.ToPagedList(nextPage, 1);

            return View(pageViewModels);
        }
    }
}
