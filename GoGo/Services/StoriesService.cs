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
            model.AuthorId = user.Id;
            
            var story = mapper.Map<Story>(model);
            story.Author = user;
            
            await this.storiesRepository.AddAsync(story);
            await this.storiesRepository.SaveChangesAsync();
        }

        public StoryViewModel GetDetails(string id)
        {
            var story = this.storiesRepository.All().FirstOrDefault(x => x.Id == id);
            
            story.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(x => x.StoryId == id).ToList();

            var mod = mapper.Map<StoryViewModel>(story);

            mod.Author = this.usersRepository.All().FirstOrDefault(u => u.Id == story.AuthorId).FirstName;
            
            return mod;
        }

        public async Task LikeStory(string id, GoUser user) //id(storyId)
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

            await this.peopleStoriesRepository.SaveChangesAsync();
            await this.storiesRepository.SaveChangesAsync();
        }
    }
}
