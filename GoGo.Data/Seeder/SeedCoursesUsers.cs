using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data.Seeder
{
    public class SeedCoursesUsers
    {
        public static async Task Seed(IServiceProvider provider, GoDbContext context)
        {
            if (!context.CoursesUsers.Any())
            {
                var allCourses = context.Courses.ToList();

                foreach (var course in allCourses)
                {
                    var countParticipans = new Random().Next(1, 20);

                    for (int k = 0; k < countParticipans; k++)
                    {
                        var statusUser = new Random().Next(1, 3);
                        var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();

                        var courseUser = new CoursesUsers
                        {
                            ParticipantId = randomUser.Id,
                            Participant = randomUser,
                            CourseId = course.Id,
                            Course = course,
                            StatusUser = (StatusParticitant)statusUser
                        };
                        var userExist = context.CoursesUsers
                            .FirstOrDefault(x => x.CourseId == course.Id && x.ParticipantId == randomUser.Id);

                        if (userExist == null)
                        {
                            await context.CoursesUsers.AddAsync(courseUser);
                            await context.SaveChangesAsync();
                        }
                    }

                }


            }
        }
    }
}
