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
    internal class CoursesUsersRepositoryBuilder
    {
        public Mock<IRepository<CoursesUsers>> CoursesUsersRepoMock { get; }

        public CoursesUsersRepositoryBuilder()
        {
            this.CoursesUsersRepoMock = new Mock<IRepository<CoursesUsers>>();
        }

        public static IQueryable<CoursesUsers> CreateCoursesUsers()
        {
            return new List<CoursesUsers>
            {
                 new CoursesUsers
                {
                    CourseId = "1",
                    ParticipantId = "8",
                    StatusUser = StatusParticitant.Successfully
                },
                 new CoursesUsers
                {
                    CourseId = "1",
                    ParticipantId = "9",
                    StatusUser = StatusParticitant.Successfully
                },
                 new CoursesUsers
                {
                    CourseId = "1",
                    ParticipantId = "11",
                    StatusUser = StatusParticitant.Unsuccessfully
                },
                 new CoursesUsers
                {
                    CourseId = "2",
                    ParticipantId = "11",
                    StatusUser = StatusParticitant.Unsuccessfully
                },
                  new CoursesUsers
                {
                    CourseId = "4",
                    ParticipantId = "7",
                    StatusUser = StatusParticitant.Unsuccessfully
                },
            }.AsQueryable();
        }

        internal CoursesUsersRepositoryBuilder WithAll()
        {
            CoursesUsersRepoMock.Setup(d => d.All())
                .Returns(CreateCoursesUsers).Verifiable();

            return this;
        }

        internal IRepository<CoursesUsers> Build()
        {
            return CoursesUsersRepoMock.Object;
        }
    }
}
