using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class CoursesRepositoryBuilder
    {
        public Mock<IRepository<Course>> CoursesRepoMock { get; }

        public CoursesRepositoryBuilder()
        {
            this.CoursesRepoMock = new Mock<IRepository<Course>>();
        }
        
        internal CoursesRepositoryBuilder WithAll()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Id = "1",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    DurationOfDays = 3,
                    CreatorId = "7",
                    Status = Status.Practically,
                    Category = Category.Climbing
                },
                 new Course
                {
                    Id = "2",
                    Image = new byte[0],
                    Title = "Brum",
                    Description = "Brummmmmmmmmmmmm",
                    MaxCountParticipants = 10,
                    StartDate = DateTime.Now.AddDays(2),
                    DurationOfDays = 5,
                    CreatorId = "9",
                    Status = Status.Theoretical,
                    Category = Category.Cycling
                },
                new Course
                {
                    Id = "3",
                    Image = new byte[0],
                    Title = "Mrun",
                    Description = "Mrunnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 11,
                    StartDate = DateTime.Now.AddDays(1),
                    DurationOfDays = 7,
                    Status = Status.Practically,
                    Category = Category.Skiing
                }
            }.AsQueryable();

            CoursesRepoMock.Setup(d => d.All())
                .Returns(courses).Verifiable();

            return this;
        }

        internal IRepository<Course> Build()
        {
            return CoursesRepoMock.Object;
        }
    }
}
