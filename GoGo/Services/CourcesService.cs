using GoGo.Data;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services
{
    public class CourcesService : ICourcesService
    {
        private readonly IRepository<CourcesUsers> courcesUsersRepository;
        private readonly IRepository<Cource> courcesRepository;
        private readonly IRepository<GoUser> usersRepository;

        public CourcesService(IRepository<CourcesUsers> courcesUsersRepository, 
                                IRepository<Cource> courcesRepository, 
                                IRepository<GoUser> usersRepository)
        {
            this.courcesUsersRepository = courcesUsersRepository;
            this.courcesRepository = courcesRepository;
            this.usersRepository = usersRepository;
        }

        public void AddCource(CreateCourceViewModel model, GoUser user)
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

            var cource = new Cource
            {
                Image = file,
                Title = model.Title,
                StartDate = model.StartDate,
                MaxCountParticipants = model.MaxCountParticipants,
                DurationOfDays = model.DurationOfDays,
                CountOfHours = model.CountOfHours,
                Description = model.Description,
                Status = model.Status,
                Category = model.Category,
                Creator = user,
                CreatorId = user.Id
            };

            this.courcesRepository.AddAsync(cource);
            this.courcesRepository.SaveChangesAsync();
        }

        public void AddResultToUsersCourses(UsersResultsViewModel model)
        {
            var userCouse = this.courcesUsersRepository.All()
                .FirstOrDefault(x => x.CourceId == model.CourceId && x.ParticipantId == model.ParticipantId);

            userCouse.StatusUser = model.Result;

            this.courcesUsersRepository.SaveChangesAsync();
        }

        public void AddUserToCource(string id, GoUser user)
        {
            var cource = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var userCource = new CourcesUsers
            {
                ParticipantId = user.Id,
                Participant = user,
                CourceId = cource.Id,
                Cource = cource
            };

            this.courcesUsersRepository.AddAsync(userCource);
            this.courcesUsersRepository.SaveChangesAsync();
        }

        public ICollection<CourceViewModel> GetAllCources()
        {
            var cources = this.courcesRepository.All().ToList();

            var courceModels = new List<CourceViewModel>();

            foreach (var c in cources)
            {
                var model = new CourceViewModel
                {
                    Id = c.Id,
                    Image = c.Image,
                    Title = c.Title,
                    StartDate = c.StartDate,
                    MaxCountParticipants = c.MaxCountParticipants,
                    DurationOfDays = c.DurationOfDays,
                    CountOfHours = c.CountOfHours,
                    Description = c.Description,
                    Status = c.Status,
                    Category = c.Category
                };

                courceModels.Add(model);
            }

            return courceModels;
        }

        public ICollection<UsersResultsViewModel> GetAllParticipants(string id)
        {
            var usersResult = this.courcesUsersRepository.All().Where(x => x.CourceId == id)
                                    .Select(x => new UsersResultsViewModel
                                    {
                                        CourceId = id,
                                        ParticipantId = x.ParticipantId,
                                        Participant = new GoUserViewModel
                                        {
                                            Id = x.ParticipantId,
                                            FirstName = x.Participant.FirstName,
                                            Image = x.Participant.Image
                                        },
                                        Result = x.StatusUser
                                        //Results = new List<ResultViewModel>
                                        //{
                                        //    new ResultViewModel { Id = (int)StatusParticitant.Successfully, ResultName = StatusParticitant.Successfully.ToString() },
                                        //    new ResultViewModel { Id = (int)StatusParticitant.Unsuccessfully, ResultName = StatusParticitant.Unsuccessfully.ToString() }
                                        //}.ToList()
                                    })
                                    .ToList();
            return usersResult;

        }

        public CourceViewModel GetDetails(string id)
        {
            var cource = courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var creatorr = this.usersRepository.All().FirstOrDefault(x => x.Id == cource.CreatorId);

            var creator = new GoUserViewModel
            {
                Id = cource.CreatorId,
                FirstName = creatorr.FirstName,
                Image = creatorr.Image,
            };

            var participents = this.courcesUsersRepository.All()
                                            .Where(x => x.CourceId == id)
                                            .Select(x => new GoUserViewModel
                                            {
                                                Id = x.ParticipantId,
                                                FirstName = x.Participant.FirstName,
                                                Image = x.Participant.Image
                                            }).ToList();

            var model = new CourceViewModel
            {
                Id = cource.Id,
                Image = cource.Image,
                Title = cource.Title,
                StartDate = cource.StartDate,
                MaxCountParticipants = cource.MaxCountParticipants,
                DurationOfDays = cource.DurationOfDays,
                CountOfHours = cource.CountOfHours,
                Description = cource.Description,
                Status = cource.Status,
                Category = cource.Category,
                FreeSeats = cource.MaxCountParticipants - cource.Participants.Count(),
                Participants = participents,
                Creator = creator,
            };

            return model;
        }
    }
}
