using AutoMapper;
using GoGo.Data;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services.Contracts;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository<Destination> destRepository;
        private readonly IRepository<DestinationsUsers> destUsersRepository;
        private readonly IRepository<PeopleStories> peopleStoriesRepository;
        private readonly IRepository<Comment> commentsRepository;
        private readonly IRepository<Story> storiesRepository;
        private readonly IRepository<GoUser> usersRepository;
        private UserManager<GoUser> userManager;
        private readonly IMapper mapper;
        
        public DestinationService(IRepository<Destination> destRepository,
                                    IRepository<DestinationsUsers> destUsersRepository,
                                    IRepository<PeopleStories> peopleStoriesRepository,
                                    IRepository<Comment> commentsRepository,
                                    IRepository<Story> storiesRepository,
                                    IRepository<GoUser> usersRepository,
                                    UserManager<GoUser> userManager,
                                    IMapper mapper)
        {
            this.destRepository = destRepository;
            this.destUsersRepository = destUsersRepository;
            this.peopleStoriesRepository = peopleStoriesRepository;
            this.commentsRepository = commentsRepository;
            this.storiesRepository = storiesRepository;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public void AddDestination(CreateDestinationViewModel model, GoUser user)
        {
            byte[] file = null;
            if (model.Image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    model.Image.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            var dest = mapper.Map<Destination>(model);
            dest.Image = file;
            dest.Creator = user;
            dest.CreatorId = user.Id;
            
            this.destRepository.AddAsync(dest);
            destRepository.SaveChangesAsync();
        }

        public async Task AddSocialization(GoUser user, string id, string socialization)
        {
            this.destUsersRepository.All()
                .FirstOrDefault(x => x.DestinationId == id && x.ParticipantId == user.Id)
                .Socialization = Enum.Parse<Socialization>(socialization);

            await this.destUsersRepository.SaveChangesAsync();
        }

        public DestUserViewModel AddUserToDestination(GoUser user, string id) //destinationId
        {
            var destination = this.destRepository.All().FirstOrDefault(x => x.Id == id);
            
            var destUserModel = new DestUserViewModel
            {
                Destination = destination,
                DestinationId = destination.Id,
                Participant = user,
                ParticipantId = user.Id
            };

            var destinationUser = new DestinationsUsers
            {
                Destination = destination,
                DestinationId = destination.Id,
                Participant = user,
                ParticipantId = user.Id
            };
           // var destUsModel = this.Mapp<DestUserViewModel>(destinationUser);

            this.destUsersRepository.AddAsync(destinationUser);
            this.destUsersRepository.SaveChangesAsync();

            return destUserModel;
        }

        public ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization)
        {
            var usersNotKnowAnyone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone")
                                            .Select(x => mapper.Map<GoUserViewModel>(user)).ToList();
                                            
            if (socialization == "NotKnowAnyone")
            {
                var notIncludeYourself = usersNotKnowAnyone.FirstOrDefault(x => x.Id == user.Id);
                usersNotKnowAnyone.Remove(notIncludeYourself);
            }

            return usersNotKnowAnyone;
        }

        public ICollection<DestViewModel> GetAllDestinations()
        {
            var destinationsModels = this.destRepository.All().Select(x => mapper.Map<DestViewModel>(x)).ToList();
            
            return destinationsModels;
        }

        public DestDetailsViewModel GetDetails(string id, string userName) //GoUser user)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.UserName == userName);

            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);

            var usersNotKnowAnyone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone"/* && x.ParticipantId != user.Id*/)
                                            .Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();
                                            
            var usersKnowSomeone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "KnowSomeone" /*&& x.ParticipantId != user.Id*/)
                                            .Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();
            
            var goUserModel = mapper.Map<CurrentUserViewModel>(user);
            
            var allComments = this.commentsRepository.All().Where(x => x.DestinationId == id)
                .Select(x => mapper.Map<CommentViewModel>(x)).ToList();

            allComments.ForEach(x => x.Comentator =
            mapper.Map<GoUserViewModel>(this.usersRepository.All().FirstOrDefault(c => c.Id == x.ComentatorId)));
            
            var allStories = this.storiesRepository.All().Where(x => x.DestinationId == id)
                .Select(x => mapper.Map<StoryViewModel>(x)).ToList();
            
            allStories.ForEach(x => x.PeopleWhosLikeThis = this.peopleStoriesRepository.All().Where(s=>s.StoryId == x.Id).Count());

            allStories.ForEach(x => x.Author = this.usersRepository.All().FirstOrDefault(u => u.Id == x.AuthorId).FirstName);
            
            var model = mapper.Map<DestDetailsViewModel>(dest);
            model.Creator = this.usersRepository.All().FirstOrDefault(x => x.Id == dest.CreatorId).FirstName;
            model.CurrentUser = goUserModel;
            model.AllComments = allComments;
            model.Stories = allStories;
            model.ParticipantsKnowSomeone = usersKnowSomeone;
            model.ParticipantsNotKnowAnyone = usersNotKnowAnyone;
            
            return model;
        }
    }
}
