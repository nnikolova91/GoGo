using GoGo.Data;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Destinations
{
    public/* static*/ class SeedDestinations
    {
        private const string DestinationsFilePath = @"..\GoGo.Data\Seeder\Destinations\DataSeed\Destinations.txt";
        private const string DestinationsImagesPath = @"..\GoGo.Data\Seeder\Destinations\ImagesSeed\";
        //private readonly IServiceProvider provider;

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            var sb = new StringBuilder();
            var dataDestinations = new List<string>();

            if (!context.Destinations.Any())
            {
                var owners = System.IO.File.ReadAllLines(DestinationsFilePath);


                for (int i = 0; i < owners.Length; i++)
                {
                    if (owners[i] == "")
                    {
                        var dest = sb.ToString();
                        dataDestinations.Add(dest);
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sb.AppendLine(owners[i]);
                    }
                }

                var destinations = new List<Destination>();

                for (int i = 0; i < dataDestinations.Count; i++)
                {
                    var titleAndContent = dataDestinations[i].Split(Environment.NewLine).ToArray();

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

                    var description = string.Join(Environment.NewLine, titleAndContent.Skip(1));
                    var imgPath = DestinationsImagesPath + titleImg.ToString() + ".jpg";

                    var creator = context.Users.OrderBy(x => Guid.NewGuid()).First();

                    var random = new Random().Next(1, 10);
                    var random1 = new Random().Next(1, 7);
                    var random2 = new Random().Next(1, 6);

                    var endDateToJoin = DateTime.Now.AddDays(random - random1);
                    var startDate = DateTime.Now.AddDays(random);
                    var endDate = DateTime.Now.AddDays(random + random2);

                    var randomLevel = new Random().Next(1, 3);

                    var destination = new Destination
                    {
                        Naame = title,
                        Description = description,
                        Image = File.ReadAllBytes(imgPath),
                        Creator = creator,
                        CreatorId = creator.Id,
                        StartDate = startDate,
                        EndDateToJoin = endDateToJoin,
                        EndDate = endDate,
                        Level = (LevelOfDifficulty)randomLevel
                    };

                    await context.Destinations.AddAsync(destination);
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
}
