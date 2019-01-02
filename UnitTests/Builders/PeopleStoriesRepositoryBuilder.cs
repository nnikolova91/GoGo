using GoGo.Data.Common;
using GoGo.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class PeopleStoriesRepositoryBuilder
    {
        public Mock<IRepository<PeopleStories>> PeopleStoriesRepoMock { get; }

        public PeopleStoriesRepositoryBuilder()
        {
            this.PeopleStoriesRepoMock = new Mock<IRepository<PeopleStories>>();
        }

        internal PeopleStoriesRepositoryBuilder WithAll()
        {
            var peopleStories = new List<PeopleStories>
            {
                new PeopleStories
                {
                    UserId ="9",
                    StoryId = "7"
                },
                new PeopleStories
                {
                    UserId ="10",
                    StoryId = "7"
                },
                new PeopleStories
                {
                    UserId ="11",
                    StoryId = "9"
                },
            }.AsQueryable();

            PeopleStoriesRepoMock.Setup(d => d.All())
                .Returns(peopleStories).Verifiable();

            return this;
        }

        internal IRepository<PeopleStories> Build()
        {
            return PeopleStoriesRepoMock.Object;
        }
    }
}
