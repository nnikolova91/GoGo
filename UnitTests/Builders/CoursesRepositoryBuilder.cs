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
        public Mock<IRepository<Cource>> CoursesRepoMock { get; }

        public CoursesRepositoryBuilder()
        {
            this.CoursesRepoMock = new Mock<IRepository<Cource>>();
        }
        
        internal CoursesRepositoryBuilder WithAll()
        {
            var courses = new List<Cource>
            {
                new Cource
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
                 new Cource
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
                new Cource
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

        internal IRepository<Cource> Build()
        {
            return CoursesRepoMock.Object;
        }
    }
}
