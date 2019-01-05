using GoGo.Data.Common;
using GoGo.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class ThemsRepositoryBuilder
    {
        public Mock<IRepository<Theme>> ThemsRepoMock { get; }

        public ThemsRepositoryBuilder()
        {
            this.ThemsRepoMock = new Mock<IRepository<Theme>>();
        }
        
        internal ThemsRepositoryBuilder WithAll()
        {
            var thems = new List<Theme>
            { 
                new Theme
                {
                    Id = "1",
                    Description = "Niki otiva na more",
                    Date = DateTime.Parse("07/02/2018"),
                    AuthorId = "7"
                },
                new Theme
                {
                    Id = "2",
                    Description = "Niki otiva na planina",
                    Date = DateTime.Now.AddDays(-2),
                    AuthorId = "8"
                },
                new Theme
                {
                    Id = "3",
                    Description = "Na planinaaaaa",
                    Date = DateTime.Now.AddDays(-3),
                    AuthorId = "9"
                }
            }.AsQueryable();

        ThemsRepoMock.Setup(d => d.All())
                .Returns(thems).Verifiable();

            return this;
        }

        internal IRepository<Theme> Build()
        {
            return ThemsRepoMock.Object;
        }
    }
}
