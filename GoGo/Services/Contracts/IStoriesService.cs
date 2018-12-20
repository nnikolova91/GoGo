using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services.Contracts
{
    public interface IStoriesService
    {
        Task AddStory(CreateStoryViewModel model, string id, GoUser user);

        StoryViewModel GetDetails(string id);

        Task LikeStory(string id, GoUser user);

        ICollection<StoryViewModel> AllMyStories(GoUser user);

        ICollection<StoryViewModel> AllStories();
    }
}
