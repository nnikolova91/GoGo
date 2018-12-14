using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Stories
{
    public class SeedStories
    {
        private const string StoriesFilePath = @"..\GoGo.Data\Seeder\Stories\DataSeed\Stories.txt";

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            var stories = System.IO.File.ReadAllLines(StoriesFilePath);

            if (!context.Stories.Any())
            {
                var allDests = context.Destinations.ToList();

                foreach (var dest in allDests)
                {
                    var countStories = new Random().Next(1, 20);

                    for (int k = 0; k < countStories; k++)
                    {
                        //var socialization = new Random().Next(1, 3);
                        var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();

                        var stor = stories.OrderBy(x => Guid.NewGuid()).First();
                        var title = String.Join(" ", stor.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3));

                        if (!context.Stories.Any(x=>x.Content == stor && x.DestinationId == dest.Id))
                        {
                            var story = new Story
                            {
                                Author = randomUser,
                                AuthorId = randomUser.Id,
                                Destination = dest,
                                DestinationId = dest.Id,
                                Content = stor,
                                Title = title
                            };

                            await context.Stories.AddAsync(story);
                            await context.SaveChangesAsync();
                        }
                        
                    };

                }

            }
        }
    }
}
