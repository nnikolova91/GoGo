using GoGo.Data;
using GoGo.Models;
using GoGo.Services.Contracts;
using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services
{
    public class GamesService : IGamesService
    {
        private readonly GoDbContext context;
        
        public GamesService(GoDbContext context)
        {
            this.context = context;
        }

        public void AddGame(CreateGameViewModel model)
        {
            var game = new Game
            {
                Name = model.Name,
                Description = model.Description
            };

            this.context.Games.Add(game);
            //this.context.SaveChanges();
        }
    }
}
