using GoGo.Data;
using GoGo.Models;
using GoGo.Services.Contracts;
using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace GoGo.Services
{
    public class GamesService : IGamesService
    {
        private readonly GoDbContext context;
        private readonly IMapper mapper;

        public GamesService(GoDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<string> AddGame(CreateGameViewModel model)
        {
            byte[] file = ImageAsBytes(model.Level1.Image);

            var game = new Game
            {
                Name = model.Name,
                Description = model.Description
            };

            this.context.Games.Add(game);
            await this.context.SaveChangesAsync();
            return game.Id;
            
            
        }

        private static byte[] ImageAsBytes(IFormFile image)
        {
            byte[] file = null;
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            return file;
        }

        public async Task AddLevelsToGame(string gameId, CreateGameViewModel model)
        {
            var game = this.context.Games.FirstOrDefault(x => x.Id == gameId);

            var level1 = new Level
            {
                Description = model.Level1.Description,
                Points = model.Level1.Points,
                GameId = game.Id,
                Image = ImageAsBytes(model.Level1.Image)
            };

            this.context.Levels.Add(level1);

            this.context.Levels.Add(new Level
            {
                Description = model.Level2.Description,
                Points = model.Level2.Points,
                GameId = game.Id,
                Image = ImageAsBytes(model.Level2.Image)
            });

            game.Image = level1.Image;

            await this.context.SaveChangesAsync();
        }

        public ICollection<GameViewModel> GetAllGames()
        {
            var gamesModels = this.context.Games.Select(x=>new GameViewModel{Id = x.Id,Name = x.Name, Image = x.Image}).ToList();

            return gamesModels;
        }

        public GameDetailsViewModel GetDetails(string id)
        {
            var game = context.Games.FirstOrDefault(x => x.Id == id);

            var levels = context.Levels.Where(x => x.GameId == id).ToList();

            var levelss = levels.Select(x => mapper.Map<LevelViewModel>(x)).ToList();

            var gameParticipantsLevel1 = this.context.LevelsParticipants
                .Where(l => l.GameId == id && l.LevelId == levels[0].Id).Select(x => new GameLevelParticipantViewModel
                {
                    GameId = x.GameId,
                    LevelId = x.LevelId,
                    ParticipantId = x.ParticipantId,
                    CorrespondingImage = x.CorrespondingImage,
                    IsPassed = x.IsPassed
                }).ToList();

            var gameParticipantsLevel2 = this.context.LevelsParticipants
                .Where(l => l.GameId == id && l.LevelId == levels[1].Id).Select(x => new GameLevelParticipantViewModel
                {
                    GameId = x.GameId,
                    LevelId = x.LevelId,
                    ParticipantId = x.ParticipantId,
                    CorrespondingImage = x.CorrespondingImage,
                    IsPassed = x.IsPassed
                }).ToList();
            
            var gameModel = new GameDetailsViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Level1 = levelss[0],
                Level2 = levelss[1],
                GameParticipantsLevel1 = gameParticipantsLevel1,
                GameParticipantsLevel2 = gameParticipantsLevel2
            };

            return gameModel;
                //.Select(x => new LevelViewModel { Description = x.Description, Points = x.Points, Image = ImageAsBytes(x.Image), GameId = x.GameId });
        }

        public async Task UserStartGame(string id, GoUser user)
        {
            var game = this.context.Games.FirstOrDefault(x => x.Id == id);
            var levels = this.context.Levels.Where(x => x.GameId == id).ToList();

            if (this.context.LevelsParticipants.FirstOrDefault(x=>x.ParticipantId == user.Id && x.GameId == game.Id) == null)
            {
                var gamesLevelsUsers = levels.Select(x => new GameLevelParticipant
                {
                    GameId = game.Id,
                    Game = game,
                    ParticipantId = user.Id,
                    LevelId = x.Id,
                    Level = x,
                    IsPassed = false
                });

                await this.context.AddRangeAsync(gamesLevelsUsers);

                await this.context.SaveChangesAsync();
            }
        }

        public async Task UserAddImageToLevel(string id, GoUser user, string levelId, IFormFile image)
        {
            var levelPartisipant = this.context.LevelsParticipants
                .FirstOrDefault(x => x.GameId == id && x.ParticipantId == user.Id && x.LevelId == levelId);

            if (levelPartisipant != null)
            {
                levelPartisipant.CorrespondingImage = ImageAsBytes(image);

                await this.context.SaveChangesAsync();
            }
        }
    }
}
