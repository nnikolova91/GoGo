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

        public void AddUserToDestination(GoUser user, string id)
        {
            var destination = this.context.Destinations.FirstOrDefault(x => x.Id == id);

            var destinationUser = new DestinationsUsers
            {
                Destination = destination,
                DestinationId = destination.Id,
                Participant = user,
                ParticipantId = user.Id
            };

            this.context.DestinationsUsers.Add(destinationUser);
            this.context.SaveChanges();
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

        public DestDetailsViewModel GetDetails(string id)
        {
            var dest = this.context.Destinations.FirstOrDefault(x => x.Id == id);

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
                Participants = this.context.DestinationsUsers.Where(x => x.DestinationId == id)
                                    .Select(x => new GoUserViewModel
                                    {
                                        FirstName = x.Participant.FirstName,
                                        Image = x.Participant.Image
                                    })
                                    .ToList()
            };

            return model;
        }

        public ICollection<GoUserViewModel> Socializer(string socialization, string id) //destinationId
        {
            var s = Enum.Parse<Socialization>(socialization);
            var usersNotKnowSomething = new List<GoUserViewModel>();

            if (socialization == "KnowSomeone")
            {
                usersNotKnowSomething = this.context.DestinationsUsers
                                                    .Where(x => x.Socialization != s && x.DestinationId == id)
                                                    .Select(x => new GoUserViewModel
                                                    {
                                                        Image = x.Participant.Image,
                                                        FirstName = x.Participant.FirstName
                                                    })
                                                    .ToList();
            }

            usersNotKnowSomething = this.context.DestinationsUsers
                                                   .Where(x => x.Socialization == s && x.DestinationId == id)
                                                   .Select(x => new GoUserViewModel
                                                   {
                                                       Image = x.Participant.Image,
                                                       FirstName = x.Participant.FirstName
                                                   })
                                                   .ToList();

            return usersNotKnowSomething;
        }
    }
}
