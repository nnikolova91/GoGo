using GoGo.Models;
using GoGo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using UnitTests.Comparers;
using ViewModels;
using Xunit;

namespace UnitTests
{
    public class ThemsTests : BaseTests
    {
        [Fact]
        public async Task AddThem_ShouldAddNewThemCorrectly()
        {
            var themsRepoBuilder = new ThemsRepositoryBuilder();
            var themsRepo = themsRepoBuilder
                .WithAll()
                .Build();

            var sut = new ThemService(themsRepo, null, null, Mapper);

            var model = new CreateThemViewModel
            {
                Description = "TraLaLA"
            };

            var user = new GoUser { Id = "7" };

            await sut.AddThem(model, user);

            themsRepoBuilder.ThemsRepoMock.Verify(r => r.AddAsync(It.IsAny<Theme>()), Times.Once);
            themsRepoBuilder.ThemsRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddCommentToThem_ShouldAddNewThemComment_Correctly()
        {
            var themsRepoBuilder = new ThemsRepositoryBuilder();
            var themsRepo = themsRepoBuilder
                .WithAll()
                .Build();

            var themCommentRepoBuilder = new ThemCommentsRepositoryBuilder();
            var themCommentsRepo = themCommentRepoBuilder
                .WithAll()
                .Build();

            var sut = new ThemService(themsRepo, null, themCommentsRepo, Mapper);

            var model = new CreateThemViewModel
            {
                Description = "TraLaLA"
            };

            var user = new GoUser { Id = "7" };

            await sut.AddCommentToThem("3", "New themComment", user);

            themCommentRepoBuilder.ThemCommentsRepoMock.Verify(r => r.AddAsync(It.IsAny<ThemComment>()), Times.Once);
            themCommentRepoBuilder.ThemCommentsRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddCommentToThem_ShouldThrow_IfThemNotExist()
        {
            var themsRepoBuilder = new ThemsRepositoryBuilder();
            var themsRepo = themsRepoBuilder
                .WithAll()
                .Build();

            var themCommentRepoBuilder = new ThemCommentsRepositoryBuilder();
            var themCommentsRepo = themCommentRepoBuilder
                .WithAll()
                .Build();

            var sut = new ThemService(themsRepo, null, themCommentsRepo, Mapper);

            var model = new CreateThemViewModel
            {
                Description = "TraLaLA"
            };

            var user = new GoUser { Id = "7" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddCommentToThem("5", "New themComment", user));

            Assert.Equal("Them not exist!", ex.Message);

            themCommentRepoBuilder.ThemCommentsRepoMock.Verify(r => r.AddAsync(It.IsAny<ThemComment>()), Times.Never);
            themCommentRepoBuilder.ThemCommentsRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_ThemDetailsViewModel()
        {
            var themsRepoBuilder = new ThemsRepositoryBuilder();
            var themsRepo = themsRepoBuilder
                .WithAll()
                .Build();

            var themCommentRepoBuilder = new ThemCommentsRepositoryBuilder();
            var themCommentsRepo = themCommentRepoBuilder
                .WithAll()
                .Build();

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var sut = new ThemService(themsRepo, usersRepo, themCommentsRepo, Mapper);

            var user = new GoUser { Id = "7" };

            var actual = sut.GetDetails("1", user);

            var expected = new ThemDetailsViewModel
            {
                Id = "1",
                Description = "Niki otiva na more",
                Date = DateTime.Parse("07/02/2018"),
                AuthorId = "7",
                Author = "Slavqna ",
                Comments = new List<ThemCommentViewModel>
                {
                    new ThemCommentViewModel
                    {
                        Id = "6",
                        Content = "Niki otiva na more",
                        ThemId = "1",
                        Date = DateTime.Parse("08/02/2018"),
                        AuthorId = "11",
                        Author = "Koni "
                    },
                    new ThemCommentViewModel
                    {
                        Id = "7",
                        Content = "Nikiiiiiiiiiii",
                        ThemId = "1",
                        Date = DateTime.Parse("09/02/2018"),
                        AuthorId = "8",
                        Author = "Niki "
                    },
                }
            };

            Assert.Equal(expected, actual, new ThemDetailsViewModelComparer());
            Assert.Equal(expected.Comments, actual.Comments, new ThemCommentViewModelComparer());

            themsRepoBuilder.ThemsRepoMock.Verify();
            themCommentRepoBuilder.ThemCommentsRepoMock.Verify();
            usersRepoBuilder.UsersRepoMock.Verify();
        }

        [Fact]
        public void GetDetails_ShouldThrow_IfThemNotExist()
        {
            var themsRepoBuilder = new ThemsRepositoryBuilder();
            var themsRepo = themsRepoBuilder
                .WithAll()
                .Build();

            var themCommentRepoBuilder = new ThemCommentsRepositoryBuilder();
            var themCommentsRepo = themCommentRepoBuilder
                .WithAll()
                .Build();

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var usersRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var sut = new ThemService(themsRepo, usersRepo, themCommentsRepo, Mapper);

            var user = new GoUser { Id = "7" };
            
            var ex = Assert.Throws<ArgumentException>(() => sut.GetDetails("7", user));
            
            Assert.Equal("Them not exist!", ex.Message);
            
            themsRepoBuilder.ThemsRepoMock.Verify();
        }
    }
}
