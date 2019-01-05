using GoGo.Data.Common;
using GoGo.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class ThemCommentsRepositoryBuilder
    {
        public Mock<IRepository<ThemComment>> ThemCommentsRepoMock { get; }

        public ThemCommentsRepositoryBuilder()
        {
            this.ThemCommentsRepoMock = new Mock<IRepository<ThemComment>>();
        }

        internal ThemCommentsRepositoryBuilder WithAll()
        {
            var themComments = new List<ThemComment>
            {
                new ThemComment
                {
                    Id = "6",
                    Content = "Niki otiva na more",
                    ThemId = "1",
                    Date = DateTime.Parse("08/02/2018"),
                    AuthorId = "11"
                },
                new ThemComment
                {
                    Id = "7",
                    Content = "Nikiiiiiiiiiii",
                    ThemId = "1",
                    Date = DateTime.Parse("09/02/2018"),
                    AuthorId = "8"
                },
                new ThemComment
                {
                    Id = "8",
                    Content = "Sasoooooooooo",
                    ThemId = "2",
                    Date = DateTime.Now.AddHours(-3),
                    AuthorId = "10"
                },
                new ThemComment
                {
                    Id = "9",
                    Content = "Koniiiiiiiii",
                    ThemId = "2",
                    Date = DateTime.Now.AddHours(-4),
                    AuthorId = "9"
                },
                new ThemComment
                {
                    Id = "10",
                    Content = "Alex",
                    ThemId = "3",
                    Date = DateTime.Now.AddHours(-5),
                    AuthorId = "7"
                }
            }.AsQueryable();

            ThemCommentsRepoMock.Setup(d => d.All())
                    .Returns(themComments).Verifiable();

            return this;
        }

        internal IRepository<ThemComment> Build()
        {
            return ThemCommentsRepoMock.Object;
        }
    }
}
