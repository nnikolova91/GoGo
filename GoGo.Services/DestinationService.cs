using AutoMapper;
using GoGo.Data;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Http;
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

        public async Task AddDestination(CreateDestinationViewModel model, GoUser user)
        {
            byte[] file = NewMethod(model.Image);

            var dest = mapper.Map<Destination>(model);
            dest.Image = file;
            dest.Creator = user;
            dest.CreatorId = user.Id;

            await this.destRepository.AddAsync(dest);
            await this.destRepository.SaveChangesAsync();
        }

        private static byte[] NewMethod(IFormFile image)
        {
            byte[] file = null;
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            return file;
        }

        public async Task AddSocialization(GoUser user, string id, string socialization)
        {
            var dest = this.destUsersRepository.All()
                .FirstOrDefault(x => x.DestinationId == id && x.ParticipantId == user.Id);

            if (dest == null)
            {
                throw new ArgumentException("You are not in this group.");
            }

            dest.Socialization = Enum.Parse<Socialization>(socialization);

            await this.destUsersRepository.SaveChangesAsync();
        }

        public async Task<DestUserViewModel> AddUserToDestination(GoUser user, string id) //destinationId
        {
            var destination = this.destRepository.All().FirstOrDefault(x => x.Id == id);
            if (destination == null)
            {
                throw new ArgumentException("Destination not found.");
            }
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

            if (this.destUsersRepository.All().FirstOrDefault(x => x.DestinationId == destinationUser.DestinationId &&
                    x.ParticipantId == destinationUser.ParticipantId) != null)
            {
                throw new ArgumentException("You are in this group.");
            }
            // var destUsModel = mapper.Map<DestUserViewModel>(destinationUser);

            await this.destUsersRepository.AddAsync(destinationUser);
            await this.destUsersRepository.SaveChangesAsync();

            return destUserModel;
        }

        //public ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization)
        //{
        //    var usersNotKnowAnyone = this.destUsersRepository.All()
        //                                    .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone")
        //                                    .Select(x => mapper.Map<GoUserViewModel>(user)).ToList();

        //    //if (socialization == "NotKnowAnyone")
        //    //{
        //    //    var notIncludeYourself = usersNotKnowAnyone.FirstOrDefault(x => x.Id == user.Id);
        //    //    usersNotKnowAnyone.Remove(notIncludeYourself);
        //    //}

        //    return usersNotKnowAnyone;
        //}

        public ICollection<DestViewModel> GetAllDestinations()
        {
            var destinationsModels = this.destRepository.All().Select(x => mapper.Map<DestViewModel>(x)).ToList();
            foreach (var item in destinationsModels)
            {
                string firstChars = new string(item.Description.Take(270).ToArray());
                item.Description = firstChars + " ...";
            }

            return destinationsModels;
        }

        public DestDetailsViewModel GetDetails(string id, GoUser user) //GoUser user)
        {
            //var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userr.Id);

            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);

            if (dest == null)
            {
                throw new ArgumentException("Destination not exist");
            }

            var usersNotKnowAnyone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id &&
                                             x.Socialization.ToString() == "NotKnowAnyone"/* && x.ParticipantId != user.Id*/)
                                            .Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();

            var usersKnowSomeone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id &&
                                             x.Socialization.ToString() == "KnowSomeone" /*&& x.ParticipantId != user.Id*/)
                                            .Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();

            var goUserModel = mapper.Map<CurrentUserViewModel>(user);

            var allComments = this.commentsRepository.All().Where(x => x.DestinationId == id)
                                            .Select(x => mapper.Map<CommentViewModel>(x)).ToList();

            allComments.ForEach(x => x.Comentator = mapper.Map<GoUserViewModel>(this.usersRepository
                                            .All().FirstOrDefault(c => c.Id == x.ComentatorId)));

            var allStories = this.storiesRepository.All().Where(x => x.DestinationId == id)
                                            .Select(x => mapper.Map<StoryViewModel>(x)).ToList();

            allStories.ForEach(x => x.PeopleWhosLikeThis = this.peopleStoriesRepository.All()
                                            .Where(s => s.StoryId == x.Id).Count());

            allStories.ForEach(x => x.Author = this.usersRepository.All()
                                            .FirstOrDefault(u => u.Id == x.AuthorId).FirstName);

            var model = mapper.Map<DestDetailsViewModel>(dest);
            model.Creator = this.usersRepository.All().FirstOrDefault(x => x.Id == dest.CreatorId).FirstName;
            model.CurrentUser = goUserModel;
            model.AllComments = allComments.OrderByDescending(x=>x.Date).ToList();
            model.Stories = allStories;
            model.ParticipantsKnowSomeone = usersKnowSomeone;
            model.ParticipantsNotKnowAnyone = usersNotKnowAnyone;

            return model;
        }

        public ICollection<DestViewModel> FindMyDestinations(GoUser user)
        {
            var destModels = this.destUsersRepository.All().Where(x => x.ParticipantId == user.Id)
                .Select(d => mapper.Map<DestViewModel>(d.Destination)).ToList();

            return destModels;
        }

        public EditDestinationViewModel FindEditDestination(string id, GoUser user)
        {
            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);

            var destination = mapper.Map<EditDestinationViewModel>(dest);

            if (dest.CreatorId == user.Id)
            {
                return destination;
            }
            else
            {
                throw new ArgumentException("You can not edit this page");
            }
        }

        public async Task EditDestination(EditDestinationViewModel model)
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

            var destination = this.destRepository.All().FirstOrDefault(x => x.Id == model.Id);

            if (destination != null)
            {
                destination.Image = file;
                destination.Level = model.Level;
                destination.StartDate = model.StartDate;
                destination.EndDate = model.EndDate;
                destination.EndDateToJoin = model.EndDateToJoin;
                destination.Naame = model.Naame;
                destination.Description = model.Description;

                await this.destRepository.SaveChangesAsync();
            }

        }

        public DestViewModel FindToDeleteDestination(string id, GoUser user)
        {
            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);

            if (dest.CreatorId != user.Id)
            {
                throw new ArgumentException("You can not delete this page");
            }

            var destination = mapper.Map<DestViewModel>(dest);

            return destination;
        }

        public async Task DeleteDestinationsUsers(string id)
        {
            var destUsers = this.destUsersRepository.All().Where(x => x.DestinationId == id).ToList();

            destUsers.ForEach(x => this.destUsersRepository.Delete(x));

            await this.destUsersRepository.SaveChangesAsync();
        }

        public async Task DeleteComments(string id)
        {
            var comments = this.commentsRepository.All().Where(x => x.DestinationId == id).ToList();

            comments.ForEach(x => this.commentsRepository.Delete(x));

            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteFromStories(string id)
        {
            var stories = this.storiesRepository.All().Where(x => x.DestinationId == id).ToList();

            foreach (var item in stories)
            {
                item.DestinationId = null;
                item.Destination = null;
            }

            await this.storiesRepository.SaveChangesAsync();
        }

        public async Task DeleteDestination(string id)
        {
            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);

            this.destRepository.Delete(dest);

            await this.destRepository.SaveChangesAsync();
        }
    }
}
