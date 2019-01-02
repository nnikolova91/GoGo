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
    internal class GoUserRepositoryBuilder
    {
        public Mock<IRepository<GoUser>> UsersRepoMock { get; }

        public GoUserRepositoryBuilder()
        {
            this.UsersRepoMock = new Mock<IRepository<GoUser>>();
        }

        internal GoUserRepositoryBuilder WithAll()
        {
            var users = new List<GoUser>
            {
                new GoUser
                {
                    Id = "7",
                    FirstName = "Slavqna",
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new GoUser
                {
                    Id = "8",
                    FirstName = "Niki",
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new GoUser
                {
                    Id = "9",
                    FirstName = "Pelin",
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new GoUser
                {
                    Id = "10",
                    FirstName = "Saso",
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new GoUser
                {
                    Id = "11",
                    FirstName = "Koni",
                    Image = ConvertImageToByteArray(SetupFileMock().Object)
                },
                new GoUser
                {
                    Id = "12",
                    FirstName = "Alex"
                },
            }.AsQueryable();

            UsersRepoMock.Setup(d => d.All())
                .Returns(users).Verifiable();

            return this;
        }

        internal IRepository<GoUser> Build()
        {
            return UsersRepoMock.Object;
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
