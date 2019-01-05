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
using GoGo.Models.Enums;
using GoGo.Data.Common;

namespace GoGo.Services
{
    public class GamesService : IGamesService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Game> gamesRepository;
        private readonly IRepository<Level> levelsRepository;
        private readonly IRepository<GameLevelParticipant> levelsParticipantRepository;
        private readonly IRepository<GoUser> usersRepository;

        public GamesService(IRepository<Game> gamesRepository,
                                IRepository<Level> levelsRepository,
                                IRepository<GameLevelParticipant> levelsParticipantRepository,
                                IRepository<GoUser> usersRepository,
                                IMapper mapper)
        {
            this.gamesRepository = gamesRepository;
            this.levelsRepository = levelsRepository;
            this.levelsParticipantRepository = levelsParticipantRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task<string> AddGame(CreateGameViewModel model, GoUser user)
        {

            byte[] imageLevel1 = ImageAsBytes(model.Level1.Image);
            byte[] imageLevel2 = ImageAsBytes(model.Level2.Image);
            byte[] imageLevel3 = ImageAsBytes(model.Level3.Image);

            var game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                Image = imageLevel3,
                CreatorId = user.Id
            };

            await this.gamesRepository.AddAsync(game);
            await this.gamesRepository.SaveChangesAsync();

            var level1 = new Level
            {
                Image = imageLevel1,
                Description = model.Level1.Description,
                NumberInGame = model.Level1.NumberInGame,
                GameId = game.Id,
                Game = game,
                Points = model.Level1.Points
            };
            var level2 = new Level
            {
                Image = imageLevel2,
                Description = model.Level2.Description,
                NumberInGame = model.Level2.NumberInGame,
                GameId = game.Id,
                Game = game,
                Points = model.Level2.Points
            };
            var level3 = new Level
            {
                Image = imageLevel3,
                Description = model.Level3.Description,
                NumberInGame = model.Level3.NumberInGame,
                GameId = game.Id,
                Game = game,
                Points = model.Level3.Points
            };

            await this.levelsRepository.AddAsync(level1);
            await this.levelsRepository.AddAsync(level2);
            await this.levelsRepository.AddAsync(level3);

            await this.levelsRepository.SaveChangesAsync();

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

        public ICollection<GameViewModel> GetAllGames()
        {
            var gamesModels = this.gamesRepository.All()
                .Select(x => mapper.Map<GameViewModel>(x)).ToList();

            return gamesModels;
        }

        public GameDetailsViewModel GetDetails(string id)
        {
            var game = this.gamesRepository.All().FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                throw new ArgumentException("Game do not exist!");
            }

            var levels = levelsRepository.All().Where(x => x.GameId == id).OrderBy(x => x.NumberInGame).ToList();

            var levelss = levels.Select(x => mapper.Map<LevelViewModel>(x))
                .OrderBy(x => x.NumberInGame).ToList();

            var gameParticipantsLevel1 = this.levelsParticipantRepository.All()
                .Where(l => l.GameId == id && l.LevelId == levels[0].Id && l.StatusLevel == StatusLevel.NotPassed)
                .Select(x => mapper.Map<GameLevelParticipantViewModel>(x)).ToList();

            gameParticipantsLevel1.ForEach(x => x.Participant = this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).FirstName + " " + this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).LastName);

            var gameParticipantsLevel2 = this.levelsParticipantRepository.All()
                                            .Where(l => l.GameId == id && l.LevelId == levels[1].Id && l.StatusLevel == StatusLevel.NotPassed)
                                            .Select(x => mapper.Map<GameLevelParticipantViewModel>(x)).ToList();

            gameParticipantsLevel2.ForEach(x => x.Participant = this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).FirstName + " " + this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).LastName);

            var gameParticipantsLevel3 = this.levelsParticipantRepository.All()
                                            .Where(l => l.GameId == id && l.LevelId == levels[2].Id && l.StatusLevel == StatusLevel.NotPassed)
                                            .Select(x => mapper.Map<GameLevelParticipantViewModel>(x)).ToList();

            gameParticipantsLevel3.ForEach(x => x.Participant = this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).FirstName + " " + this.usersRepository
                                            .All().FirstOrDefault(u => u.Id == x.ParticipantId).LastName);

            var gameModel = new GameDetailsViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Creator = this.usersRepository.All().FirstOrDefault(x => x.Id == game.CreatorId).FirstName,
                Level1 = levelss[0],
                Level2 = levelss[1],
                Level3 = levelss[2],
                GameParticipantsLevel1 = gameParticipantsLevel1,
                GameParticipantsLevel2 = gameParticipantsLevel2,
                GameParticipantsLevel3 = gameParticipantsLevel3
            };

            return gameModel;
            //.Select(x => new LevelViewModel { Description = x.Description, Points = x.Points, Image = ImageAsBytes(x.Image), GameId = x.GameId });
        }

        public async Task UserStartGame(string id, GoUser user)
        {
            var game = this.gamesRepository.All().FirstOrDefault(x => x.Id == id);
            var levels = this.levelsRepository.All().Where(x => x.GameId == id).OrderBy(x => x.NumberInGame).ToList();

            if (game == null)
            {
                throw new ArgumentException("Game do not exist!");
            }

            else if (this.levelsParticipantRepository.All().FirstOrDefault(x => x.ParticipantId == user.Id && x.GameId == game.Id) == null)
            {
                var gamesLevelsUsers = levels.Select(x => new GameLevelParticipant
                {
                    GameId = game.Id,
                    Game = game,
                    ParticipantId = user.Id,
                    LevelId = x.Id,
                    Level = x,
                    StatusLevel = (StatusLevel)2
                }).ToList();

                await this.levelsParticipantRepository.AddRangeAsync(gamesLevelsUsers);

                await this.levelsParticipantRepository.SaveChangesAsync();
            }
        }

        public async Task UserAddImageToLevel(string id, GoUser user, string levelId, IFormFile image)
        {
            var levelPartisipant = this.levelsParticipantRepository.All()
                .FirstOrDefault(x => x.GameId == id && x.ParticipantId == user.Id && x.LevelId == levelId);

            if (levelPartisipant != null && levelPartisipant.StatusLevel == StatusLevel.SuccessfullyPassed)
            {
                throw new ArgumentException("You are auready pass successfully this level!");
            }

            if (levelPartisipant != null)
            {
                levelPartisipant.CorrespondingImage = ImageAsBytes(image);

                await this.levelsParticipantRepository.SaveChangesAsync();
            }
        }

        public async Task AddLevelResult(GameLevelParticipantViewModel model, GoUser user)
        {
            var gameLevelParticipant = this.levelsParticipantRepository.All()
                .FirstOrDefault(x => x.GameId == model.GameId && x.LevelId == model.LevelId
                && x.ParticipantId == model.ParticipantId);

            if (gameLevelParticipant == null)
            {
                throw new ArgumentException("This GameLeveelParticipant not exist!");
            }

            gameLevelParticipant.StatusLevel = model.StatusLevel;

            if (model.StatusLevel == StatusLevel.SuccessfullyPassed)
            {
                var participant = this.usersRepository.All().FirstOrDefault(x => x.Id == model.ParticipantId);

                var level = this.levelsRepository.All().FirstOrDefault(l => l.Id == model.LevelId);

                participant.Points += level.Points;

                await this.usersRepository.SaveChangesAsync();

                //this.levelsParticipantRepository.Delete(gameLevelParticipant);
            }

            await this.levelsParticipantRepository.SaveChangesAsync();
        }
    }
}
