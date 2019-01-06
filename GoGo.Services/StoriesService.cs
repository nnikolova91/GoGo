using AutoMapper;
using GoGo.Data;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services
{
    public class StoriesService : IStoriesService
    {
        public const string DestinationNotExist = "Destination not exist!";
        public const string StoryNotExist = "Story not exist!";
        public const string SpaceSeparetedUsersFirstAndLastName = " ";

        private readonly IRepository<Story> storiesRepository;
        private readonly IRepository<Destination> destinationsRepository;
        private readonly IRepository<PeopleStories> peopleStoriesRepository;
        private readonly IRepository<GoUser> usersRepository;
        private readonly IMapper mapper;

        public StoriesService(IRepository<Story> storiesRepository,
            IRepository<Destination> destinationsRepository,
            IRepository<PeopleStories> peopleStoriesRepository,
            IRepository<GoUser> usersRepository,
            IMapper mapper)
        {
            this.storiesRepository = storiesRepository;
            this.destinationsRepository = destinationsRepository;
            this.peopleStoriesRepository = peopleStoriesRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task AddStory(CreateStoryViewModel model, string id, GoUser user) //id(destinationId)
        {
            var destination = this.destinationsRepository.All().FirstOrDefault(x => x.Id == id);
            if (destination == null)
            {
                throw new ArgumentException(DestinationNotExist);
            }

            model.AuthorId = user.Id;

            var story = mapper.Map<Story>(model);
            story.Author = user;

            await this.storiesRepository.AddAsync(story);
            await this.storiesRepository.SaveChangesAsync();
        }

        public ICollection<StoryViewModel> AllMyStories(GoUser user)
        {
            var myStories = this.storiesRepository.All().Where(x => x.AuthorId == user.Id)
               .Select(x => mapper.Map<StoryViewModel>(x)).ToList();

            foreach (var item in myStories)
            {
                item.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(x => x.StoryId == item.Id).Count();
                var name = this.usersRepository.All()
                    .FirstOrDefault(x => x.Id == item.AuthorId);
                item.Author = name.FirstName + SpaceSeparetedUsersFirstAndLastName + name.LastName;
            }

            return myStories;
        }

        public ICollection<StoryViewModel> AllStories()
        {
            var allStories = this.storiesRepository.All()
               .Select(x => mapper.Map<StoryViewModel>(x)).ToList();

            foreach (var item in allStories)
            {
                item.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(x => x.StoryId == item.Id).Count();
                var name = this.usersRepository.All()
                    .FirstOrDefault(x => x.Id == item.AuthorId);
                item.Author = name.FirstName + SpaceSeparetedUsersFirstAndLastName + name.LastName;
            }

            return allStories.OrderByDescending(x => x.PeopleWhosLikeThis).ToList();
        }

        public StoryViewModel GetDetails(string id)
        {
            var story = this.storiesRepository.All().FirstOrDefault(x => x.Id == id);

            if (story == null)
            {
                throw new ArgumentException(StoryNotExist);
            }

            story.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(x => x.StoryId == id).ToList();

            var mod = mapper.Map<StoryViewModel>(story);

            mod.Author = this.usersRepository.All().FirstOrDefault(u => u.Id == story.AuthorId).FirstName;

            return mod;
        }

        public async Task LikeStory(string id, GoUser user) //id(storyId)
        {
            var story = this.storiesRepository.All().FirstOrDefault(x => x.Id == id);

            if (story == null)
            {
                throw new ArgumentException(StoryNotExist);
            }
            var userStory = new PeopleStories
            {
                Story = story,
                StoryId = story.Id,
                User = user,
                UserId = user.Id

            };

            story.PeopleWhosLikeThis.Add(userStory);

            if (peopleStoriesRepository.All().FirstOrDefault(x => x.StoryId == userStory.StoryId && x.UserId == userStory.UserId) == null)
            {
                await this.peopleStoriesRepository.AddAsync(userStory);

                await this.peopleStoriesRepository.SaveChangesAsync();
                await this.storiesRepository.SaveChangesAsync();
            }
        }
    }
}
