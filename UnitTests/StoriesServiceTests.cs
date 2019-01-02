using GoGo.Models;
using GoGo.Services;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using UnitTests.Comparers;
using ViewModels;
using Xunit;

namespace UnitTests
{
    public class StoriesServiceTests : BaseTests
    {
        [Fact]
        public async Task AddStory_ShouldAddNewStoryCorrectly()
        {
            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();


            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, destRepo, null, null, Mapper);

            var createStoryViewModel = new CreateStoryViewModel
            {
                Content = "Niki otiva na ski",
                Title = "Niki",
                DestinationId = "2",
            };

            var user = new GoUser { Id = "7" };

            await sut.AddStory(createStoryViewModel, "2", user);

            storiesRepoBuilder.StoriesRepoMock.Verify(r => r.AddAsync(It.IsAny<Story>()), Times.Once);
            storiesRepoBuilder.StoriesRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddStory_ShouldThrow_IfDestinationNotExist()
        {
            var destRepoBuilder = new DestinationsRepositoryBuilder();
            var destRepo = destRepoBuilder
                .WithAll()
                .Build();


            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, destRepo, null, null, Mapper);

            var createStoryViewModel = new CreateStoryViewModel
            {
                Content = "Niki otiva na ski",
                Title = "Niki",
                DestinationId = "11",
            };

            var user = new GoUser { Id = "7" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddStory(createStoryViewModel, "11", user));

            Assert.Equal("Destination not exist!", ex.Message);

            storiesRepoBuilder.StoriesRepoMock.Verify(r => r.AddAsync(It.IsAny<Story>()), Times.Never);
            storiesRepoBuilder.StoriesRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void/*async Task*/ AllMyStories_ShouldReturnCorrectListStoryViewModels()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();
            
            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var UserStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = UserStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, userRepo, Mapper);

            var user = new GoUser { Id = "10" };

            var actual = sut.AllMyStories(user);

            var expected = new List<StoryViewModel>
            {
                new StoryViewModel
                {
                    Id = "1",
                    DestinationId = "3",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "2",
                    DestinationId = "3",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "3",
                    DestinationId = "6",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "4",
                    DestinationId = "4",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "5",
                    DestinationId = "5",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "6",
                    DestinationId = "1",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "9",
                    DestinationId = "2",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Brum"
                }
            };

            Assert.Equal(expected, actual, new StoriesViewModelComparer());
        }

        [Fact]
        public void/*async Task*/ AllStories_ShouldReturnCorrectListOfAllStoryViewModels()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();

            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var UserStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = UserStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, userRepo, Mapper);
            
            var actual = sut.AllStories();

            var expected = new List<StoryViewModel>
            {
                 new StoryViewModel
                {
                    Id = "7",
                    DestinationId = "2",
                    AuthorId = "8",
                    Author = "Niki ",
                    Title = "Mrun",

                },
                 new StoryViewModel
                {
                    Id = "9",
                    DestinationId = "2",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Brum"
                },
                new StoryViewModel
                {
                    Id = "1",
                    DestinationId = "3",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "2",
                    DestinationId = "3",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "3",
                    DestinationId = "6",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "4",
                    DestinationId = "4",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "5",
                    DestinationId = "5",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "6",
                    DestinationId = "1",
                    AuthorId = "10",
                    Author = "Saso ",
                    Title = "Drun"
                },
                new StoryViewModel
                {
                    Id = "8",
                    DestinationId = "2",
                    AuthorId = "9",
                    Author = "Pelin ",
                    Title = "Drun"
                },
                
            }.AsQueryable();
            
            Assert.Equal(expected, actual, new StoriesViewModelComparer());
        }

        [Fact]
        public void GetDetails_ShouldThrow_IfStoryNotExist()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();

            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var UserStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = UserStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, userRepo, Mapper);
            
            var ex = Assert.Throws<ArgumentException>(() => sut.GetDetails("17"));

            Assert.Equal("Story not exist!", ex.Message);
        }

        [Fact]
        public void GetDetails_ShouldReturnCorrect_StoryViewModel()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();

            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var UserStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = UserStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, userRepo, Mapper);

            var actual = sut.GetDetails("7");

            var peopleStories = userStoriesRepo.All().Where(x => x.StoryId == "7").Select(x => Mapper.Map<StoryViewModel>(x.Story)).ToList();

            var author = userRepo.All().FirstOrDefault(x => x.Id == "8");

            var expected = new StoryViewModel
            {
                Id = "7",
                DestinationId = "2",
                AuthorId = "8",
                Author = author.FirstName,
                Title = "Mrun",
                PeopleWhosLikeThis = peopleStories.Count()
            };

            Assert.Equal(expected, actual, new StoriesViewModelComparer());
        }

        [Fact]
        public async Task LikeStory_ShouldAddNew_UserStory()
        {
            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var userStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = userStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, null, Mapper);

            var user = new GoUser { Id = "7" };

            await sut.LikeStory("2", user);

            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(r => r.AddAsync(It.IsAny<PeopleStories>()), Times.Once);
            
            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(r => r.AddAsync(It.IsAny<PeopleStories>()), Times.Once);
        }

        [Fact]
        public async Task LikeStory_ShouldThrow_IfStoryNotExist()
        {
            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var userStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = userStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, null, Mapper);

            var user = new GoUser { Id = "7" };
            
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.LikeStory("17", user));
            
            Assert.Equal("Story not exist!", ex.Message);
            
            storiesRepoBuilder.StoriesRepoMock.Verify();

            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(d => d.AddAsync(It.IsAny<PeopleStories>()), Times.Never);
            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task LikeStory_ShouldNotAddUserStory_IfUserStoryAureadyExist()
        {
            var storiesRepoBuilder = new StoriesRepositoryBuilder();
            var storiesRepo = storiesRepoBuilder
                .WithAll()
                .Build();

            var userStoriesRepoBuilder = new PeopleStoriesRepositoryBuilder();
            var userStoriesRepo = userStoriesRepoBuilder
                .WithAll()
                .Build();

            var sut = new StoriesService(storiesRepo, null, userStoriesRepo, null, Mapper);

            var user = new GoUser { Id = "9" };

            await sut.LikeStory("7", user);

            //Assert.Equal("Story not exist!", ex.Message);

            storiesRepoBuilder.StoriesRepoMock.Verify();

            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(d => d.AddAsync(It.IsAny<PeopleStories>()), Times.Never);
            userStoriesRepoBuilder.PeopleStoriesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }
    }

}
