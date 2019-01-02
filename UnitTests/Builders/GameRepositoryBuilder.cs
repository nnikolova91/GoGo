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
    internal class GameRepositoryBuilder
    {
        public Mock<IRepository<Game>> GamesRepoMock { get; }

        public GameRepositoryBuilder()
        {
            this.GamesRepoMock = new Mock<IRepository<Game>>();
        }

        internal GameRepositoryBuilder WithAll()
        {
            var games = new List<Game>
            {
                new Game
                {
                    Id = "7",
                    Name = "Nameri mqstoto",
                    Description = "nameriiiiiiiiiii",
                    CreatorId = "8",
                },
                 new Game
                {
                    Id = "9",
                    Name = "Na more",
                    Description = "moreeeeeeeeeeeeeeeeeeee"
                },
            }.AsQueryable();

            GamesRepoMock.Setup(d => d.All())
                .Returns(games).Verifiable();

            return this;
        }

        internal IRepository<Game> Build()
        {
            return GamesRepoMock.Object;
        }

        private static byte[] ConvertImageToByteArray(IFormFile image)
        {
            byte[] file = null;
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            return file;
        }

        internal static Mock<IFormFile> SetupFileMock()
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
