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
        private readonly IRepository<Story> storiesRepository;
        private readonly IRepository<Destination> destinationsRepository;
        private readonly IRepository<PeopleStories> peopleStoriesRepository;
        private readonly IRepository<GoUser> usersRepository;

        public StoriesService(IRepository<Story> storiesRepository,
            IRepository<Destination> destinationsRepository,
            IRepository<PeopleStories> peopleStoriesRepository,
            IRepository<GoUser> usersRepository)
        {
            this.storiesRepository = storiesRepository;
            this.destinationsRepository = destinationsRepository;
            this.peopleStoriesRepository = peopleStoriesRepository;
            this.usersRepository = usersRepository;
        }

        public void AddStory(StoryViewModel model, string id, GoUser user) //id(destinationId)
        {
            var destination = this.destinationsRepository.All().FirstOrDefault(x => x.Id == id);

            var story = new Story
            {
                Title = model.Title,
                Content = model.Content,
                Destination = this.destinationsRepository.All().FirstOrDefault(x=>x.Id == id),
                DestinationId = id,
                Author = user,
                AuthorId = user.Id
            };

            this.storiesRepository.AddAsync(story);
            this.storiesRepository.SaveChangesAsync();
        }

        public StoryViewModel GetDetails(string id)
        {
            var story = this.storiesRepository.All().FirstOrDefault(x => x.Id == id);
            story.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(x => x.StoryId == id).ToList();

            var model = new StoryViewModel
            {
                Id = story.Id,
                DestinationId = story.DestinationId,
                Title = story.Title,
                Content = story.Content,
                PeopleWhosLikeThis = story.PeopleWhosLikeThis.Count(),
                Author = this.usersRepository.All().FirstOrDefault(x=>x.Id == story.AuthorId).FirstName,
                AuthorId = story.AuthorId
            };
            return model;
        }

        public void LikeStory(string id, GoUser user) //id(storyId)
        {
            var story = this.storiesRepository.All().FirstOrDefault(x => x.Id == id);

            var userStory = new PeopleStories
            {
                Story = story,
                StoryId = story.Id,
                User = user,
                UserId = user.Id
                
            };

            story.PeopleWhosLikeThis.Add(userStory);

            this.peopleStoriesRepository.SaveChangesAsync();
            this.storiesRepository.SaveChangesAsync();
        }
    }
}
