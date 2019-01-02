using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services.Contracts
{
    public interface IUserStoriesService
    {
        ICollection<StoryViewModel> AllMyStories(GoUser user);
    }
}
