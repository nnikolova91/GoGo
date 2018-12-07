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
        void AddStory(StoryViewModel model, string id, GoUser user);

        StoryViewModel GetDetails(string id);

        void LikeStory(string id, GoUser user);
    }
}
