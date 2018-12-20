using GoGo.Data.Seeder;
using GoGo.Data.Seeder.Comments;
using GoGo.Data.Seeder.Courses;
using GoGo.Data.Seeder.Destinations;
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
            await SeedCourses.Seed(serviceProvider, context);
            await SeedUsers.Seed(serviceProvider, context, userManager, roleManager);
            await SeedDestinations.Seed(serviceProvider, context);
            await SeedDestinationsUsers.Seed(serviceProvider, context);
            await SeedComments.Seed(serviceProvider, context);
            await SeedStories.Seed(serviceProvider, context);
           

            // if (await userManager.FindByNameAsync("a") == null)
            // {
            //     var user = new GoUser
            //     {
            //         UserName = "a@a.a",
            //         Email = "a@a.a",
            //         FirstName = "Adam",
            //         LastName = "Aldridge",
            //         Street = "Fake St",
            //         City = "Vancouver",
            //         Province = "BC",
            //         PostalCode = "V5U K8I",
            //         Country = "Canada",
            //         PhoneNumber = "6902341234"
            //     };
            //
            //     var result = await userManager.CreateAsync(user);
            //     if (result.Succeeded)
            //     {
            //         await userManager.AddPasswordAsync(user, password);
            //         await userManager.AddToRoleAsync(user, role1);
            //     }
            //     adminId1 = user.Id;
            // }
            //
            // if (await userManager.FindByNameAsync("b") == null)
            // {
            //     var user = new GoUser
            //     {
            //         UserName = "b@b.b",
            //         Email = "b@b.b",
            //         FirstName = "Bob",
            //         LastName = "Barker",
            //         Street = "Vermont St",
            //         City = "Surrey",
            //         Province = "BC",
            //         PostalCode = "V1P I5T",
            //         Country = "Canada",
            //         PhoneNumber = "7788951456"
            //     };
            //
            //     var result = await userManager.CreateAsync(user);
            //     if (result.Succeeded)
            //     {
            //         await userManager.AddPasswordAsync(user, password);
            //         await userManager.AddToRoleAsync(user, role1);
            //     }
            //     adminId2 = user.Id;
            // }
            //
            // if (await userManager.FindByNameAsync("m") == null)
            // {
            //     var user = new GoUser
            //     {
            //         UserName = "m@m.m",
            //         Email = "m@m.m",
            //         FirstName = "Mike",
            //         LastName = "Myers",
            //         Street = "Yew St",
            //         City = "Vancouver",
            //         Province = "BC",
            //         PostalCode = "V3U E2Y",
            //         Country = "Canada",
            //         PhoneNumber = "6572136821"
            //     };
            //
            //     var result = await userManager.CreateAsync(user);
            //     if (result.Succeeded)
            //     {
            //         await userManager.AddPasswordAsync(user, password);
            //         await userManager.AddToRoleAsync(user, role2);
            //     }
            // }
            //
            // if (await userManager.FindByNameAsync("d") == null)
            // {
            //     var user = new GoUser
            //     {
            //         UserName = "d@d.d",
            //         Email = "d@d.d",
            //         FirstName = "Donald",
            //         LastName = "Duck",
            //         Street = "Well St",
            //         City = "Vancouver",
            //         Province = "BC",
            //         PostalCode = "V8U R9Y",
            //         Country = "Canada",
            //         PhoneNumber = "6041234567"
            //     };
            //
            //     var result = await userManager.CreateAsync(user);
            //     if (result.Succeeded)
            //     {
            //         await userManager.AddPasswordAsync(user, password);
            //         await userManager.AddToRoleAsync(user, role2);
            //     }
            // }
        }
    }
}
