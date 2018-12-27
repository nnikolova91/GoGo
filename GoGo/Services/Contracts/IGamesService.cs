using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoGo.Models;
using Microsoft.AspNetCore.Http;

namespace GoGo.Services.Contracts
{
    public interface IGamesService
    {
        Task<string> AddGame(CreateGameViewModel model);

        Task AddLevelsToGame(string gameId, CreateGameViewModel model);

        ICollection<GameViewModel> GetAllGames();

        GameDetailsViewModel GetDetails(string id);

        Task UserStartGame(string id, GoUser user);

        Task UserAddImageToLevel(string id, GoUser user, string levelId, IFormFile image);

        Task AddLevelResult(GameLevelParticipantViewModel model);
    }
}
