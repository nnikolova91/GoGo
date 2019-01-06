using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Builders;
using UnitTests.Comparers;
using ViewModels;
using Xunit;

namespace UnitTests
{
    public class DestinationServiceTests : BaseTests
    {
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
        public async Task AddSocialization_ShouldDoNotWorkCorrectlyIfDestinationDoNotExist()
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

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddSocialization(new GoUser() { Id = "2" }, "5", "KnowSomeone"));

            ex.Message.ShouldBe("You are not in this group.");
            destUsersRepoMock.Verify();
        }


        [Fact]
        public async Task AddUserToDestination_ShouldDoNotWorkCorrectlyIfDestinationDoNotExist()
        {
            var destination = new List<Destination>
            {
                new Destination
                {
                    Id = "1"
                }
            }.AsQueryable();

            var destRepoMock = new Mock<IRepository<Destination>>();
            destRepoMock.Setup(d => d.All())
                .Returns(destination).Verifiable();

            var sut = new DestinationService(destRepoMock.Object, null, null, null, null, null, null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddUserToDestination(new GoUser() { Id = "2" }, "5"));

            ex.Message.ShouldBe("Destination not exist");
            destRepoMock.Verify();
        }

        [Fact]
        public async Task AddUserToDestination_ShouldThrowIfDestinationsUserAureadyExist()
        {
            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var destUsersRepoBuilder = new DestinationsUsersRepositoryBuilder();
            var destUserRepo = destUsersRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, destUserRepo, null, null, null, null, null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddUserToDestination(new GoUser() { Id = "3" }, "1"));

            ex.Message.ShouldBe("You are auready in this group.");
            destUsersRepoBuilder.DestUsersRepoMock.Verify();
            destUsersRepoBuilder.DestUsersRepoMock.Verify(d => d.AddAsync(It.IsAny<DestinationsUsers>()), Times.Never);
            destUsersRepoBuilder.DestUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToDestination_ShouldThrowIfEnddateToJoinIsPassed()
        {
            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var destUsersRepoBuilder = new DestinationsUsersRepositoryBuilder();
            var destUserRepo = destUsersRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, destUserRepo, null, null, null, null, null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddUserToDestination(new GoUser() { Id = "3" }, "3"));

            ex.Message.ShouldBe("The end date to join is passed");
            destUsersRepoBuilder.DestUsersRepoMock.Verify();
            destUsersRepoBuilder.DestUsersRepoMock.Verify(d => d.AddAsync(It.IsAny<DestinationsUsers>()), Times.Never);
            destUsersRepoBuilder.DestUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void GetAllDestinations_ShouldReturn_All_DestViewModels()
        {
            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, null, null, null, null, null, null, this.Mapper);

            var actual = sut.GetAllDestinations();
            var expected = new List<DestViewModel>
            {
                new DestViewModel {Id = "1" },new DestViewModel{ Id = "2" },
                new DestViewModel{ Id = "3" },new DestViewModel{ Id = "4" }
            }.AsQueryable();

            Assert.Equal(expected, actual, new DestViewModelComparer());

            destRepoBuilder.DestRepoMock.Verify();
            destRepoBuilder.DestRepoMock.Verify(d => d.AddAsync(It.IsAny<Destination>()), Times.Never);
            destRepoBuilder.DestRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void FindMyDestinations_ShouldReturn_CorrectCountOf_DestViewModels()
        {
            var user = new GoUser { Id = "1" };

            var destUserRepoBuilder = new DestinationsUsersRepositoryBuilder();
            var destUserRepo = destUserRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(null, destUserRepo, null, null, null, null, null, this.Mapper);

            var actual = sut.FindMyDestinations(user);

            var expected = new List<DestViewModel>
            {
                new DestViewModel {Id = "1" }
            }.AsQueryable();

            Assert.Equal(expected.Count(), actual.Count());

            destUserRepoBuilder.DestUsersRepoMock.Verify();
            destUserRepoBuilder.DestUsersRepoMock.Verify(d => d.AddAsync(It.IsAny<DestinationsUsers>()), Times.Never);
            destUserRepoBuilder.DestUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void GetAllDestinations_ShouldReturn_EmptyCollection_IfIsEmpty()
        {
            var destinations = new List<Destination>().AsQueryable();

            var destRepoMock = new Mock<IRepository<Destination>>();
            destRepoMock.Setup(d => d.All())
                .Returns(destinations).Verifiable();

            var sut = new DestinationService(destRepoMock.Object, null, null, null, null, null, null, this.Mapper);

            var actual = sut.GetAllDestinations();
            var expected = new List<DestViewModel>().AsQueryable();

            Assert.Equal(expected, actual, new DestViewModelComparer());

            destRepoMock.Verify();
            destRepoMock.Verify(d => d.AddAsync(It.IsAny<Destination>()), Times.Never);
            destRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToDestination_ShouldWorkCorrectlyAnd_ReturnViewModel()
        {
            var dest = new Destination
            {
                Id = "1",
                EndDateToJoin = DateTime.Now.AddDays(3)
            };

            var destination = new List<Destination>
            {
                dest
            }.AsQueryable();

            var destRepoMock = new Mock<IRepository<Destination>>();
            destRepoMock.Setup(d => d.All())
                .Returns(destination).Verifiable();

            var user = new GoUser() { Id = "2" };

            var du = new DestinationsUsers { DestinationId = "7", ParticipantId = "5" };
            var destUsers = new List<DestinationsUsers> { du }.AsQueryable();

            var destUserRepoMock = new Mock<IRepository<DestinationsUsers>>();
            destUserRepoMock.Setup(d => d.All())
                .Returns(destUsers).Verifiable();

            var sut = new DestinationService(destRepoMock.Object, destUserRepoMock.Object, null, null, null, null, null, null);

            var actual = await sut.AddUserToDestination(user, "1");

            DestUserViewModel expected = new DestUserViewModel()
            {
                DestinationId = dest.Id,
                ParticipantId = user.Id
            };

            destRepoMock.Verify();

            Assert.Equal(expected, actual, new DestUserViewModelComparer());
        }

        [Fact]
        public void FindEditDestination_ShouldReturn_EditDestinationViewModel()
        {
            var user = new GoUser { Id = "7" };

            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, null, null, null, null, null, null, Mapper);

            var actual = sut.FindEditDestination("3", user);

            Assert.IsType<EditDestinationViewModel>(actual);
        }

        [Fact]
        public async Task EditDestination_ShouldEditDestinationCorrectly()
        {

            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, null, null, null, null, null, null, Mapper);

            var editDestinationViewModel = new EditDestinationViewModel
            {
                Id = "2",
                Image = SetupFileMock().Object,
                Description = "nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn",
                Level = (LevelOfDifficulty)2,
                Naame = "Niki",
                StartDate = DateTime.Now.AddDays(3),
                EndDateToJoin = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(5)
            };


            await sut.EditDestination(editDestinationViewModel);

            var newdest = destRepo.All().FirstOrDefault(x => x.Id == "2");

            var actual = Mapper.Map<EditDestinationViewModel>(newdest);

            Assert.Equal(editDestinationViewModel, actual, new EditDestinationViewModelComparer());
        }

        [Fact]
        public void FindEditDestination_ShouldThrow_WhenUserIsNotCreator()
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

        [Fact]
        public void FindToDeleteDestination_ShouldThrow_WhenUserIsNotCreator()
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

            var ex = Assert.Throws<ArgumentException>(() => sut.FindToDeleteDestination("1", new GoUser() { Id = "2" }));

            Assert.Equal("You can not delete this page", ex.Message);
        }

        [Fact]
        public void FindToDeleteDestination_ShouldReturn_DestViewModel()
        {
            var user = new GoUser { Id = "7" };

            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, null, null, null, null, null, null, Mapper);

            var actual = sut.FindToDeleteDestination("3", user);

            Assert.IsType<DestViewModel>(actual);
        }

        [Fact]
        public async Task DeletComments_ShouldDeleteDestinationCommentsCorrectly()
        {
            List<Comment> deletedComments = new List<Comment>();
            var commentsRepoBuilder = new CommentsRepositoryBuilder();
            commentsRepoBuilder.CommentsRepoMock.Setup(r => r.Delete(It.IsAny<Comment>()))
                .Callback<Comment>(c => deletedComments.Add(c));

            var commentsRepo = commentsRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(null, null, null, commentsRepo, null, null, null, Mapper);

            await sut.DeleteComments("2");

            Assert.Equal(3, deletedComments.Count);
            deletedComments.ForEach(c => Assert.Equal("2", c.DestinationId));
            commentsRepoBuilder.CommentsRepoMock.Verify(c => c.Delete(It.IsAny<Comment>()), Times.Exactly(3));
            commentsRepoBuilder.CommentsRepoMock.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteDestinationsUsers_ShouldDeleteDestinationsUsersCorrectly()
        {
            List<DestinationsUsers> deletedDestUsers = new List<DestinationsUsers>();

            var destUserRepoBuilder = new DestinationsUsersRepositoryBuilder();
            destUserRepoBuilder.DestUsersRepoMock.Setup(r => r.Delete(It.IsAny<DestinationsUsers>()))
                .Callback<DestinationsUsers>(du => deletedDestUsers.Add(du));

            var destRepo = destUserRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(null, destRepo, null, null, null, null, null, Mapper);

            await sut.DeleteDestinationsUsers("2");

            Assert.Equal(5, deletedDestUsers.Count());
            destUserRepoBuilder.DestUsersRepoMock.Verify();
            deletedDestUsers.ForEach(c => Assert.Equal("2", c.DestinationId));

            destUserRepoBuilder.DestUsersRepoMock.Verify(d => d.Delete(It.IsAny<DestinationsUsers>()), Times.Exactly(5));
            destUserRepoBuilder.DestUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteDestination_ShouldDeleteDestinationCorrectly()
        {

            Destination destination = null;

            var destRepoBuilder = new DestinationsRepositoryBuilder();
            destRepoBuilder.DestRepoMock.Setup(r => r.Delete(It.IsAny<Destination>()))
                .Callback<Destination>(d => destination = d);

            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            var sut = new DestinationService(destRepo, null, null, null, null, null, null, Mapper);

            await sut.DeleteDestination("2");

            Assert.Equal("2", destination.Id);
            destRepoBuilder.DestRepoMock.Verify();

            destRepoBuilder.DestRepoMock.Verify(d => d.Delete(It.IsAny<Destination>()), Times.Once);
            destRepoBuilder.DestRepoMock.Verify(d => d.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_ListFromCommentViewModels_AllComments()
        {
            var user = new GoUser { Id = "1" };

            var sut = SetupGetDetailsSut();

            var actual = sut.GetDetails("2", user);

            var expected = new DestDetailsViewModel
            {
                Id = "2",
                Description = "aaa",
                Creator = "Niki",
                Image = new byte[0],
                Stories = new List<StoryViewModel>(),
                CurrentUser = Mapper.Map<CurrentUserViewModel>(user),
                AllComments = CommentsRepositoryBuilder.CreateComments().Where(x => x.DestinationId == "2").Select(x => Mapper.Map<CommentViewModel>(x)).ToList()
            };
            Assert.Equal(expected.AllComments, actual.AllComments, new CommentsViewModelComparer());
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_ListFrom_UsersNotKnowAnyone()
        {
            var user = new GoUser { Id = "1" };

            var sut = SetupGetDetailsSut();

            var actual = sut.GetDetails("2", user);

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var expected = new List<GoUserViewModel>
            {
                new GoUserViewModel{ Id = "9", FirstName = "Pelin", Image = usersRepo.All()
                .FirstOrDefault(x=>x.Id == "9").Image},

                new GoUserViewModel{ Id = "10", FirstName = "Saso", Image = usersRepo.All()
                .FirstOrDefault(x=>x.Id == "10").Image},

                new GoUserViewModel{ Id = "11", FirstName = "Koni", Image = usersRepo.All()
                .FirstOrDefault(x=>x.Id == "11").Image},
            };

            Assert.Equal(expected, actual.ParticipantsNotKnowAnyone, new GoUserViewModelComparer());
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_ListFrom_ParticipantsKnowSomeone()
        {
            var user = new GoUser { Id = "1" };

            var sut = SetupGetDetailsSut();

            var actual = sut.GetDetails("2", user);

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var expected = new List<GoUserViewModel>
            {
                new GoUserViewModel{ Id = "7", FirstName = "Slavqna", Image = ConvertImageToByteArray(SetupFileMock().Object) },//!!!! inage
                new GoUserViewModel{ Id = "8", FirstName = "Niki", Image = ConvertImageToByteArray(SetupFileMock().Object)}
            };

            Assert.Equal(expected, actual.ParticipantsKnowSomeone, new GoUserViewModelComparer());
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_ListFrom_StoriesViewModel()
        {
            var user = new GoUser { Id = "1" };

            var sut = SetupGetDetailsSut();

            var actual = sut.GetDetails("2", user);

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var expected = new List<StoryViewModel>
            {
                new StoryViewModel
                { Id = "7", DestinationId = "2", AuthorId = "8", Author = "Niki", Title = "Mrun", PeopleWhosLikeThis = 2},

                new StoryViewModel
                { Id = "8", DestinationId = "2", AuthorId = "9", Author = "Pelin", Title = "Drun", PeopleWhosLikeThis = 0 },

                new StoryViewModel
                { Id = "9", DestinationId = "2", AuthorId = "10", Author = "Saso", Title = "Brum", PeopleWhosLikeThis = 1 }
            };

            Assert.Equal(expected, actual.Stories, new StoriesViewModelComparer());
        }

        private DestinationService SetupGetDetailsSut()
        {
            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var commentsRepoBuilder = new CommentsRepositoryBuilder();
            var commentsRepo = commentsRepoBuilder
                .WithAll()
                .Build();

            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var peopleStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var peopleStoriesRepo = peopleStoriesRepoBuilder
                .WithAll()
                .Build();

            var destUsersRepoBuilder = new DestinationsUsersRepositoryBuilder();
            var destUserRepo = destUsersRepoBuilder
                .WithAll()
                .Build();

            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();

            return new DestinationService(destRepo, destUserRepo, peopleStoriesRepo, commentsRepo, storiesRepo, usersRepo, null, Mapper);
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
    }
}
