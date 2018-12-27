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
                    var countParticipans = new Random().Next(1, 20);

                    var levelsGame = context.Levels.Where(x => x.GameId == game.Id).ToList();

                    foreach (var level in levelsGame)
                    {
                        for (int k = 0; k < countParticipans; k++)
                        {
                            var imageNumber = new Random().Next(1, 3);

                            var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();
                            if (gamesLevelUsers.FirstOrDefault(x=>x.ParticipantId == randomUser.Id) == null)
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
