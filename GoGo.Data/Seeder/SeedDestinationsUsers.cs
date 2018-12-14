using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder
{
    public class SeedDestinationsUsers
    {
        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            if (!context.DestinationsUsers.Any())
            {
                var allDests = context.Destinations.ToList();

                foreach (var dest in allDests)
                {
                    var countParticipans = new Random().Next(1, 20);

                    for (int k = 0; k < countParticipans; k++)
                    {
                        var socialization = new Random().Next(1, 3);
                        var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();

                        var destUser = new DestinationsUsers
                        {
                            ParticipantId = randomUser.Id,
                            Participant = randomUser,
                            DestinationId = dest.Id,
                            Destination = dest,
                            Socialization = (Socialization)socialization
                        };
                        var userExist = context.DestinationsUsers
                            .FirstOrDefault(x => x.DestinationId == dest.Id && x.ParticipantId == randomUser.Id);

                        if (userExist == null)
                        {
                            await context.DestinationsUsers.AddAsync(destUser);
                            await context.SaveChangesAsync();
                        }
                    }

                }

               
            }
        }
    }
}
