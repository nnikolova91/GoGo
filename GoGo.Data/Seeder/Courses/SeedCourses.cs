using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder.Courses
{
    public class SeedCourses
    {
        private const string CoursesFilePath = @"..\GoGo.Data\Seeder\Courses\DataSeed\Courses.txt";
        private const string CoursesImagesPath = @"..\GoGo.Data\Seeder\Courses\ImagesSeed\";
        //private readonly IServiceProvider provider;

        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            var sb = new StringBuilder();
            var dataCourses = new List<string>();

            if (!context.Cources.Any())
            {
                var owners = System.IO.File.ReadAllLines(CoursesFilePath);


                for (int i = 0; i < owners.Length; i++)
                {
                    if (owners[i] == "")
                    {
                        var course = sb.ToString();
                        dataCourses.Add(course);
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sb.AppendLine(owners[i]);
                    }
                }

                var courses = new List<Cource>();

                for (int i = 0; i < dataCourses.Count; i++)
                {
                    var titleAndContent = dataCourses[i].Split(Environment.NewLine).ToArray();

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

                    int image;

                    var description = string.Join(Environment.NewLine, titleAndContent.Skip(1));

                    if (title == "Курс по скално катерене")
                    {
                        image = 3;
                    }
                    else if (title == "Курс Лавинна безопасност")
                    {
                        image = 2;
                    }
                    else
                    {
                        image = 1;
                    }
                    var imgPath = CoursesImagesPath +image.ToString()/*titleImg.ToString()*/ + ".jpg";

                    var creator = context.Users.OrderBy(x => Guid.NewGuid()).First();

                    var random = new Random().Next(1, 10);
                    var random1 = new Random().Next(random, random + 30);
                    
                    var startDate = DateTime.Now.AddDays(random);

                    var randomStatus = new Random().Next(1, 3);
                    var randomCategory = new Random().Next(1, 5);

                    
                    var course = new Cource
                    {
                        Title = title,
                        Description = description,
                        Image = File.ReadAllBytes(imgPath),
                        Creator = creator,
                        CreatorId = creator.Id,
                        StartDate = startDate,
                        DurationOfDays = random,
                        CountOfHours = random1,
                        Status = (Status)randomStatus,
                        Category = (Category)randomCategory
                    };

                    await context.Cources.AddAsync(course);
                }

                //context.Destinations.AddRangeAsync;
                await context.SaveChangesAsync();
            }
        }
    }
}
