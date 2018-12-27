using GoGo.Data.Seeder;
using GoGo.Data.Seeder.Comments;
using GoGo.Data.Seeder.Courses;
using GoGo.Data.Seeder.Destinations;
using GoGo.Data.Seeder.Games;
using GoGo.Data.Seeder.Stories;
using GoGo.Data.Seeder.Users;
using GoGo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Data
{
    public class DummyData
    {
        public static async Task Initialize(GoDbContext context,
                          UserManager<GoUser> userManager,
                          RoleManager<ApplicationRole> roleManager,
                          IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
            await SeedUsers.Seed(serviceProvider, context, userManager, roleManager);
            await SeedCourses.Seed(serviceProvider, context);
            await SeedCoursesUsers.Seed(serviceProvider, context);
            await SeedDestinations.Seed(serviceProvider, context);
            await SeedDestinationsUsers.Seed(serviceProvider, context);
            await SeedComments.Seed(serviceProvider, context);
            await SeedStories.Seed(serviceProvider, context);
            await SeedGames.Seed(serviceProvider, context);
            await SeedLevelGameParticipants.Seed(serviceProvider, context);
        }
    }
}
