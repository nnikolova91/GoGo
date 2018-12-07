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
        private readonly IRepository<Comment> commentsRepository;
        private readonly IRepository<Story> storiesRepository;
        private readonly IRepository<GoUser> usersRepository;
        private UserManager<GoUser> userManager;

        public DestinationService(IRepository<Destination> destRepository,
                                    IRepository<DestinationsUsers> destUsersRepository,
                                    IRepository<Comment> commentsRepository,
                                    IRepository<Story> storiesRepository,
                                    IRepository<GoUser> usersRepository,
                                    UserManager<GoUser> userManager)
        {
            this.destRepository = destRepository;
            this.destUsersRepository = destUsersRepository;
            this.commentsRepository = commentsRepository;
            this.storiesRepository = storiesRepository;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
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

            var destination = new Destination
            {
                Naame = model.Naame,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                EndDateToJoin = model.EndDateToJoin,
                Description = model.Description,
                Level = model.Level,
                Image = file,
                Creator = user,
                CreatorId = user.Id,

            };

            this.destRepository.AddAsync(destination);
            destRepository.SaveChangesAsync();
        }

        public async Task AddSocialization(GoUser user, string id, string socialization)
        {
            this.destUsersRepository.All()
                .FirstOrDefault(x => x.DestinationId == id && x.ParticipantId == user.Id)
                .Socialization = Enum.Parse<Socialization>(socialization);

            await this.destUsersRepository.SaveChangesAsync();
        }

        public DestUserViewModel AddUserToDestination(GoUser user, string id)
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

            this.destUsersRepository.AddAsync(destinationUser);
            this.destUsersRepository.SaveChangesAsync();

            return destUserModel;
        }

        public ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization)
        {
            var usersNotKnowAnyone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone")
                                            .Select(x => new GoUserViewModel
                                            {
                                                Id = x.ParticipantId,
                                                FirstName = x.Participant.FirstName,
                                                Image = x.Participant.Image
                                            }).ToList();

            if (socialization == "NotKnowAnyone")
            {
                var notIncludeYourself = usersNotKnowAnyone.FirstOrDefault(x => x.Id == user.Id);
                usersNotKnowAnyone.Remove(notIncludeYourself);
            }

            return usersNotKnowAnyone;
        }

        public ICollection<DestViewModel> GetAllDestinations()
        {
            var destinations = this.destRepository.All();

            var destinationsModels = new List<DestViewModel>();

            foreach (var dest in destinations)
            {
                var model = new DestViewModel
                {
                    Id = dest.Id,
                    StartDate = dest.StartDate,
                    Level = dest.Level,
                    Naame = dest.Naame,
                    EndDate = dest.EndDate,
                    EndDateToJoin = dest.EndDateToJoin,
                    Description = dest.Description,
                    Image = dest.Image
                };

                destinationsModels.Add(model);
            }

            return destinationsModels;
        }

        public DestDetailsViewModel GetDetails(string id, GoUser user)
        {
            var dest = this.destRepository.All().FirstOrDefault(x => x.Id == id);
            var usersNotKnowAnyone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone"/* && x.ParticipantId != user.Id*/)
                                            .Select(x => new GoUserViewModel
                                            {
                                                Id = x.ParticipantId,
                                                FirstName = x.Participant.FirstName,
                                                Image = x.Participant.Image
                                            }).ToList();
            var usersKnowSomeone = this.destUsersRepository.All()
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "KnowSomeone" /*&& x.ParticipantId != user.Id*/)
                                            .Select(x => new GoUserViewModel
                                            {
                                                Id = x.ParticipantId,
                                                FirstName = x.Participant.FirstName,
                                                Image = x.Participant.Image
                                            }).ToList();

            var goUserModel = new GoUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Image = user.Image
            };

            var allComments = this.commentsRepository.All().Where(x => x.DestinationId == id)
                .Select(x => new CommentViewModel
                {
                    Content = x.Content,
                    ComentatorId = x.ComentatorId,
                    Comentator = new GoUserViewModel { Image = x.Comentator.Image, FirstName = x.Comentator.FirstName, Id = x.Comentator.Id },
                    DestinationId = x.DestinationId
                }).ToList();

            var allStories = this.storiesRepository.All().Where(x => x.DestinationId == id)
                .Select(x => new StoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    AuthorId = x.AuthorId,
                    Author = x.Author.FirstName, //new GoUserViewModel { Id = x.AuthorId, FirstName = x.Author.FirstName, Image = x.Author.Image },
                    DestinationId = x.DestinationId,
                    PeopleWhosLikeThis = x.PeopleWhosLikeThis.Count()
                })
                .ToList();

            var model = new DestDetailsViewModel
            {
                Id = dest.Id,
                StartDate = dest.StartDate,
                Level = dest.Level,
                Naame = dest.Naame,
                EndDate = dest.EndDate,
                EndDateToJoin = dest.EndDateToJoin,
                Description = dest.Description,
                Image = dest.Image,
                Creator = this.usersRepository.All().FirstOrDefault(x => x.Id == dest.CreatorId).FirstName,
                CurrentUser = goUserModel,
                AllComments = allComments,
                Stories = allStories,
                ParticipantsKnowSomeone = usersKnowSomeone
                                    .Select(x => new GoUserViewModel
                                    {
                                        Id = x.Id,
                                        FirstName = x.FirstName,
                                        Image = x.Image
                                    })
                                    .ToList(),
                ParticipantsNotKnowAnyone = usersNotKnowAnyone
                                    .Select(x => new GoUserViewModel
                                    {
                                        Id = x.Id,
                                        FirstName = x.FirstName,
                                        Image = x.Image
                                    })
                                    .ToList()
            };

            return model;
        }


    }
}
