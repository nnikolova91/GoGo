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
        public Mock<IRepository<CourcesUsers>> CoursesUsersRepoMock { get; }

        public CoursesUsersRepositoryBuilder()
        {
            this.CoursesUsersRepoMock = new Mock<IRepository<CourcesUsers>>();
        }

        public static IQueryable<CourcesUsers> CreateCoursesUsers()
        {
            return new List<CourcesUsers>
            {
                 new CourcesUsers
                {
                    CourceId = "1",
                    ParticipantId = "8",
                    StatusUser = StatusParticitant.Successfully
                },
                 new CourcesUsers
                {
                    CourceId = "1",
                    ParticipantId = "9",
                    StatusUser = StatusParticitant.Successfully
                },
                 new CourcesUsers
                {
                    CourceId = "1",
                    ParticipantId = "11",
                    StatusUser = StatusParticitant.Unsuccessfully
                },
                 new CourcesUsers
                {
                    CourceId = "2",
                    ParticipantId = "11",
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

        internal IRepository<CourcesUsers> Build()
        {
            return CoursesUsersRepoMock.Object;
        }
    }
}
