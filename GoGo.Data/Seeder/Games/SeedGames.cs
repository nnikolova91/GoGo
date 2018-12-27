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
    public class SeedGames
    {
        private const string GamesFilePath = @"..\GoGo.Data\Seeder\Games\DataSeed\Games.txt";
        private const string GamesImagesPath = @"..\GoGo.Data\Seeder\Games\ImagesSeed\";

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            var sb = new StringBuilder();
            var dataGames = new List<string>();

            if (!context.Games.Any())
            {
                var owners = System.IO.File.ReadAllLines(GamesFilePath);


                for (int i = 0; i < owners.Length; i++)
                {
                    if (owners[i] == "")
                    {
                        var game = sb.ToString();
                        dataGames.Add(game);
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sb.AppendLine(owners[i]);
                    }
                }

                var games = new List<Game>();

                for (int i = 0; i < dataGames.Count; i++)
                {
                    var titleAndContent = dataGames[i].Split(Environment.NewLine).ToArray();

                    var title = titleAndContent[0];
                    var titleImg = new StringBuilder();
                    titleImg.Append(title);

                    if (title.Contains("\""))
                    {
                        titleImg.Clear();

                        for (int k = 0; k <= title.Length - 1; k++)
                        {
                            if (title[k] != '"')
                            {
                                titleImg.Append(title[k]);
                            }
                        }
                    }

                    var gameDescription = string.Join(Environment.NewLine, titleAndContent.Skip(1).Take(1));
                    var level1Text = string.Join(Environment.NewLine, titleAndContent.Skip(2).Take(1));
                    var level2Text = string.Join(Environment.NewLine, titleAndContent.Skip(3).Take(1));
                    var level3Text = string.Join(Environment.NewLine, titleAndContent.Skip(4).Take(1));

                    var creator = context.Users.OrderBy(x => Guid.NewGuid()).First();

                    var random = new Random().Next(1, 10);

                    var game = new Game
                    {
                        Name = title,
                        Description = gameDescription,
                        Image = File.ReadAllBytes(GamesImagesPath + "3" + ".jpg"),
                    };

                    await context.Games.AddAsync(game);
                    await context.SaveChangesAsync();


                    var level1 = new Level
                    {
                        Image = File.ReadAllBytes(GamesImagesPath + "1" + ".jpg"),
                        Description = level1Text,
                        Points = random,
                        NumberInGame = 1,
                        Game = game,
                        GameId = game.Id
                    };

                    var level2 = new Level
                    {
                        Image = File.ReadAllBytes(GamesImagesPath + "2" + ".jpg"),
                        Description = level2Text,
                        Points = random,
                        NumberInGame = 2,
                        Game = game,
                        GameId = game.Id
                    };

                    var level3 = new Level
                    {
                        Image = File.ReadAllBytes(GamesImagesPath + "3" + ".jpg"),
                        Description = level3Text,
                        Points = random,
                        NumberInGame = 3,
                        Game = game,
                        GameId = game.Id
                    };

                    context.Levels.Add(level1);
                    context.Levels.Add(level2);
                    context.Levels.Add(level3);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
