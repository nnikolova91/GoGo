using GoGo.Data.Common;
using GoGo.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class StoriesRepositoryBuilder
    {
        public Mock<IRepository<Story>> StoriesRepoMock { get; }

        public StoriesRepositoryBuilder()
        {
            this.StoriesRepoMock = new Mock<IRepository<Story>>();
        }

        public static IQueryable<Story> CreateStories()
        {
            return new List<Story>
            {
                new Story
                {
                    Id = "1",
                    DestinationId = "3",
                    AuthorId = "10",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "2",
                    DestinationId = "3",
                    AuthorId = "10",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "3",
                    DestinationId = "6",
                    AuthorId = "10",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "4",
                    DestinationId = "4",
                    AuthorId = "10",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "5",
                    DestinationId = "5",
                    AuthorId = "10",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "6",
                    DestinationId = "1",
                    AuthorId = "10",
                    Title = "Drun"
                },

                 new Story
                {
                    Id = "7",
                    DestinationId = "2",
                    AuthorId = "8",
                    Title = "Mrun",
                },
                new Story
                {
                    Id = "8",
                    DestinationId = "2",
                    AuthorId = "9",
                    Title = "Drun"
                },
                new Story
                {
                    Id = "9",
                    DestinationId = "2",
                    AuthorId = "10",
                    Title = "Brum"
                },
            }.AsQueryable();
        }

        internal StoriesRepositoryBuilder WithAll()
        {
            StoriesRepoMock.Setup(d => d.All())
                .Returns(CreateStories).Verifiable();

            return this;
        }

        internal IRepository<Story> Build()
        {
            return StoriesRepoMock.Object;
        }
    }
}
