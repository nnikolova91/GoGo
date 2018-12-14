using GoGo.Data;
using GoGo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Users
{
    public class SeedUsers
    {
        private const string DestinationsFilePath = @"..\GoGo.Data\Seeder\Users\DataSeed\Users.txt";
        private const string DestinationsImagesPath = @"..\GoGo.Data\Seeder\Users\ImagesSeed";

        //private readonly IServiceProvider provider;

        public static async Task Seed(IServiceProvider provider, GoDbContext context, UserManager<GoUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //String adminId1 = "";
            //String adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "User";
            string desc2 = "This is the members role";

            //string password = "P@$$w0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            var sb = new StringBuilder();
            var dataDestinations = new List<string>();

            if (!context.Destinations.Any())
            {
                var owners = System.IO.File.ReadAllLines(DestinationsFilePath);
                var images = Directory.GetFiles(DestinationsImagesPath);

                

                for (int i = 0; i < owners.Length; i++)
                {
                    var names = owners[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var firstName = names[0];
                    var lastName = names[1];
                    var email = firstName + "." + lastName + "@" + "gmail.com";
                    var password = firstName + "_" + lastName + "1";

                    var random = new Random().Next(0, 7);
                    var image = images[random];

                    var user = new GoUser
                    {
                        UserName = email,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Image = File.ReadAllBytes(image)
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        if (i == 0)
                        {
                            await userManager.AddToRoleAsync(user, role1);
                        }
                        else
                        {
                            await userManager.AddToRoleAsync(user, role2);
                        }

                    }
                    //context.Users.Add(user);
                }
            }
            //context.SaveChanges();
        }
    }
}
