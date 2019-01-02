using GoGo.Data.Common;
using GoGo.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class CommentsRepositoryBuilder
    {
        public Mock<IRepository<Comment>> CommentsRepoMock { get; }

        public CommentsRepositoryBuilder()
        {
            this.CommentsRepoMock = new Mock<IRepository<Comment>>();
        }

        public static IQueryable<Comment> CreateComments()
        {
            return new List<Comment>
            {
                new Comment
                {
                    Id = "7",
                    DestinationId = "2",
                    ComentatorId = "7",
                    Content = "BrumBtum"
                },
                new Comment
                {
                    Id = "8",
                    DestinationId = "2",
                    ComentatorId = "10",
                    Content = "Brummmmmmmmmm"
                },
                new Comment
                {
                    Id = "9",
                    DestinationId = "2",
                    ComentatorId = "10",
                    Content = "Pak brumm"
                },
                new Comment
                {
                    Id = "10",
                    DestinationId = "1",
                    ComentatorId = "6",
                },
                new Comment
                {
                    Id = "11",
                    DestinationId = "1",
                    ComentatorId = "9",
                },
                new Comment
                {
                    Id = "12",
                    DestinationId = "1",
                    ComentatorId = "11",
                },
            }.AsQueryable();
        }

        internal CommentsRepositoryBuilder WithAll()
        {
            CommentsRepoMock.Setup(d => d.All())
                .Returns(CreateComments).Verifiable();

            return this;
        }

        internal IRepository<Comment> Build()
        {
            return CommentsRepoMock.Object;
        }
    }
}
