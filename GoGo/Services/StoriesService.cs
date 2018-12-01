using GoGo.Data;
using GoGo.Models;
using GoGo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly GoDbContext context;

        public StoriesService(GoDbContext context)
        {
            this.context = context;
        }

        public void AddStory(string tytle, string content, string id, GoUser user)
        {
            var destination = this.context.Destinations.FirstOrDefault(x => x.Id == id);

            var story = new Story
            {
                Title = tytle,
                Content = content,
                Destination = destination,
                DestinationId = destination.Id,
                Author = user
            };

            this.context.Stories.Add(story);
        }
    }
}
