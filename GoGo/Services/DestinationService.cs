using GoGo.Data;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services.Contracts;
using GoGo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly GoDbContext context;
        private UserManager<GoUser> userManager;

        public DestinationService(GoDbContext context, UserManager<GoUser> userManager)
        {
            this.context = context;
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

            this.context.Destinations.Add(destination);
            context.SaveChanges();
        }

        public void AddSocialization(GoUser user, string id, string socialization)
        {
            this.context.DestinationsUsers
                .FirstOrDefault(x => x.DestinationId == id && x.ParticipantId == user.Id)
                .Socialization = Enum.Parse<Socialization>(socialization);

            this.context.SaveChanges();
        }

        public DestUserViewModel AddUserToDestination(GoUser user, string id)
        {
            var destination = this.context.Destinations.FirstOrDefault(x => x.Id == id);

            var destUserModel = new DestUserViewModel
            {
                Destination = destination,
                DestinationId = destination.Id,
                Participant = user,
                ParticipantId = user.Id
            };

            //var destinationUser = new DestinationsUsers
            //{
            //    Destination = destination,
            //    DestinationId = destination.Id,
            //    Participant = user,
            //    ParticipantId = user.Id
            //};

            //this.context.DestinationsUsers.Add(destinationUser);
            //this.context.SaveChanges();

            return destUserModel;
        }

        public ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization)
        {
            var usersNotKnowAnyone = this.context.DestinationsUsers
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
            var destinations = this.context.Destinations;

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
            var dest = this.context.Destinations.FirstOrDefault(x => x.Id == id);
            var usersNotKnowAnyone = this.context.DestinationsUsers
                                            .Where(x => x.DestinationId == id && x.Socialization.ToString() == "NotKnowAnyone"/* && x.ParticipantId != user.Id*/)
                                            .Select(x => new GoUserViewModel
                                            {
                                                Id = x.ParticipantId,
                                                FirstName = x.Participant.FirstName,
                                                Image = x.Participant.Image
                                            }).ToList();
            var usersKnowSomeone = this.context.DestinationsUsers
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

            var allComments = this.context.Comments.Where(x => x.DestinationId == id)
                .Select(x => new CommentViewModel
                {
                    Content = x.Content,
                    ComentatorId = x.ComentatorId,
                    Comentator = new GoUserViewModel { Image = x.Comentator.Image, FirstName = x.Comentator.FirstName, Id = x.Comentator.Id },
                    DestinationId = x.DestinationId
                }).ToList();

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
                Creator = this.context.Users.FirstOrDefault(x => x.Id == dest.CreatorId).FirstName,
                CurrentUser = goUserModel,
                AllComments = allComments,
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
