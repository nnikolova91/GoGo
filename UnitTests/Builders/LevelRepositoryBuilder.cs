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
    internal class LevelRepositoryBuilder
    {
        public Mock<IRepository<Level>> LevelsRepoMock { get; }

        public LevelRepositoryBuilder()
        {
            this.LevelsRepoMock = new Mock<IRepository<Level>>();
        }

        internal LevelRepositoryBuilder WithAll()
        {
            var levels = new List<Level>
            {
                new Level
                {
                    Id = "1",
                    Description = "mmmmm",
                    Points = 1,
                    GameId = "7",
                    NumberInGame = 1,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new Level
                {
                    Id = "2",
                    Description = "nnnnnnnnnnnnnnnn",
                    Points = 3,
                    GameId = "7",
                    NumberInGame = 2,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new Level
                {
                    Id = "3",
                    Description = "rrrrrrrrrrrrrrrrrr",
                    Points = 5,
                    GameId = "7",
                    NumberInGame = 3,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new Level
                {
                    Id = "4",
                    Description = "eeeeeeeeeeeee",
                    Points = 6,
                    GameId = "9",
                    NumberInGame = 1,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new Level
                {
                    Id = "5",
                    Description = "llllllllllllllllll",
                    Points = 7,
                    GameId = "9",
                    NumberInGame = 2,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new Level
                {
                    Id = "6",
                    Description = "kkkkkkkkkkkkkkk",
                    Points = 9,
                    GameId = "9",
                    NumberInGame = 3,
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
            }.AsQueryable();

            LevelsRepoMock.Setup(d => d.All())
                .Returns(levels).Verifiable();

            return this;
        }

        internal IRepository<Level> Build()
        {
            return LevelsRepoMock.Object;
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
