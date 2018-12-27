using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class DestinationServiceTests : BaseTests
    {
        //private static bool UserIsLoggedIn(GoUser user)
        //{

        //}

        [Fact]
        public async Task AddDestination_ShouldAddTheNewDestination()
        {
            Mock<IRepository<Destination>> destinationsRepoMock = new Mock<IRepository<Destination>>();
            var fileMock = SetupFileMock();
            var sut = new DestinationService(destinationsRepoMock.Object, null, null, null, null, null, null, Mapper);

            await sut.AddDestination(new ViewModels.CreateDestinationViewModel()
            {
                Image = fileMock.Object,

            }, new GoUser());

            destinationsRepoMock.Verify(r => r.AddAsync(It.IsAny<Destination>()), Times.Once);
            destinationsRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddSocialization_ShouldWorkCorrectly()
        {
            var destUsersAll = new List<DestinationsUsers>()
            {
                new DestinationsUsers()
                {
                    DestinationId = "1",
                    ParticipantId = "2"
                }
            }.AsQueryable();

            var destUsersRepoMock = new Mock<IRepository<DestinationsUsers>>();
            destUsersRepoMock.Setup(d => d.All())
                .Returns(destUsersAll).Verifiable();

            var sut = new DestinationService(null, destUsersRepoMock.Object, null, null, null, null, null, null);

            await sut.AddSocialization(new GoUser() { Id = "2" }, "1", "KnowSomeone");

            destUsersRepoMock.Verify();
            destUsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddSocialization_ShouldDoNotWorkCorrectlyIfUserIsNotInThisDestination()
        {
            var destUsersAll = new List<DestinationsUsers>()
            {
                new DestinationsUsers()
                {
                    DestinationId = "1",
                    ParticipantId = "2"
                }
            }.AsQueryable();

            var destUsersRepoMock = new Mock<IRepository<DestinationsUsers>>();
            destUsersRepoMock.Setup(d => d.All())
                .Returns(destUsersAll).Verifiable();

            var sut = new DestinationService(null, destUsersRepoMock.Object, null, null, null, null, null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddSocialization(new GoUser() { Id = "3" }, "1", "KnowSomeone"));

            ex.Message.ShouldBe("You are not in this group.");
            destUsersRepoMock.Verify();
        }

        [Fact]
        public void FindDestination_ShouldThrow_WhenUserIsNotCreator()
        {
            var destAll = new List<Destination>()
            {
                new Destination()
                {
                    Id = "1",
                    CreatorId = "1"
                }
            }.AsQueryable();

            var destRepoMock = new Mock<IRepository<Destination>>();
            destRepoMock.Setup(d => d.All())
                .Returns(destAll).Verifiable();

            var sut = new DestinationService(destRepoMock.Object, null, null, null, null, null, null, Mapper);

            var ex = Assert.Throws<ArgumentException>(() => sut.FindEditDestination("1", new GoUser() { Id = "2" }));

            Assert.Equal("You can not edit this page", ex.Message);
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
