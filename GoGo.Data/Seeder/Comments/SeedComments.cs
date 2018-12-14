using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Comments
{
    public class SeedComments
    {
        private const string CommentsFilePath = @"..\GoGo.Data\Seeder\Comments\DataSeed\Comments.txt";

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            var comments = System.IO.File.ReadAllLines(CommentsFilePath);

            if (!context.Comments.Any())
            {
                var allDests = context.Destinations.ToList();

                foreach (var dest in allDests)
                {
                    var countComments = new Random().Next(1, 11);

                    for (int k = 0; k < countComments; k++)
                    {
                        //var socialization = new Random().Next(1, 3);
                        var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();

                        var comment = new Comment
                        {
                            Comentator = randomUser,
                            ComentatorId = randomUser.Id,
                            Destination = dest,
                            DestinationId = dest.Id,
                            Content = comments.OrderBy(x => Guid.NewGuid()).First()
                        };

                        await context.Comments.AddAsync(comment);
                        await context.SaveChangesAsync();
                    };

                }
               
            }
        }
    }
}

