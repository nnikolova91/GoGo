using GoGo.Data.Common;
using GoGo.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    class DestinationsRepositoryBuilder
    {
        public Mock<IRepository<Destination>> DestRepoMock { get; }

        public DestinationsRepositoryBuilder()
        {
            this.DestRepoMock = new Mock<IRepository<Destination>>();
            
        }

        internal DestinationsRepositoryBuilder WithAll()
        {
            var fileMock = SetupFileMock();

            var destinations = new List<Destination>
            {
                new Destination
                {
                    Id = "1",
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },
                new Destination
                {
                    Id = "2",
                    Description = "aaa",
                    CreatorId = "12"
                },
                new Destination
                {
                    Id = "3",
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    CreatorId = "7",
                    EndDateToJoin = DateTime.Now.AddDays(-1)
                },
                new Destination
                {
                    Id = "4",
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                }
            }.AsQueryable();

            DestRepoMock.Setup(d => d.All())
                .Returns(destinations).Verifiable();
            
            return this;
        }

        internal IRepository<Destination> Build()
        {
            return DestRepoMock.Object;
        }

        private static Mock<IFormFile> SetupFileMock()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock;
        }
    }
}
