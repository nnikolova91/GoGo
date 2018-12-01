using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services.Contracts
{
    public interface IStoriesService
    {
        void AddStory(string tytle, string content, string id, GoUser user);
    }
}
