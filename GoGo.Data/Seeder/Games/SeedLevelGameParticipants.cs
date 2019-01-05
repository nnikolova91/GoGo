using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Games
{
    public class SeedLevelGameParticipants
    {
        private const string GamesImagesPath = @"..\GoGo.Data\Seeder\Games\ImagesSeed\";

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            if (!context.LevelsParticipants.Any())
            {
                var allGamess = context.Games.ToList();

                var gamesLevelUsers = new List<GameLevelParticipant>();

                foreach (var game in allGamess)
                {
                    var countParticipans = new Random().Next(1, 10);

                    var levelsGame = context.Levels.Where(x => x.GameId == game.Id).ToList();
                    for (int k = 0; k < countParticipans; k++)
                    {
                        var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();

                        foreach (var level in levelsGame.OrderBy(x => x.NumberInGame))
                        {
                            var imageNumber = new Random().Next(1, 3);

                            if (gamesLevelUsers.FirstOrDefault(x => x.ParticipantId == randomUser.Id && x.LevelId == level.Id && x.GameId == game.Id) == null)
                            {
                                var gameLevelparticipant = new GameLevelParticipant
                                {
                                    GameId = game.Id,
                                    Game = game,
                                    LevelId = level.Id,
                                    Level = level,
                                    ParticipantId = randomUser.Id,
                                    Participant = randomUser,
                                    StatusLevel = (StatusLevel)2,
                                    CorrespondingImage = File.ReadAllBytes(GamesImagesPath + imageNumber.ToString() + ".jpg")
                                };
                                gamesLevelUsers.Add(gameLevelparticipant);
                            }
                        }
                    }
                }

                await context.AddRangeAsync(gamesLevelUsers);
                await context.SaveChangesAsync();
            }
        }
    }
}
