using AutoMapper;
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
    public class UserStoriesService : IUserStoriesService
    {
        private readonly IRepository<Story> storyRepository;
        private readonly IMapper mapper;

        public UserStoriesService(IRepository<Story> storyRepository, IMapper mapper)
        {
            this.storyRepository = storyRepository;
            this.mapper = mapper;
        }

        public ICollection<StoryViewModel> AllMyStories(GoUser user)
        {
            throw new NotImplementedException();
        }
    }
}
