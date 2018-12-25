﻿using AutoMapper;
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
using X.PagedList;

namespace GoGo.Services
{
    public class CourcesService : ICourcesService
    {
        private readonly IRepository<CourcesUsers> courcesUsersRepository;
        private readonly IRepository<Cource> courcesRepository;
        private readonly IRepository<GoUser> usersRepository;
        private readonly IMapper mapper;

        public CourcesService(IRepository<CourcesUsers> courcesUsersRepository,
                                IRepository<Cource> courcesRepository,
                                IRepository<GoUser> usersRepository,
                                IMapper mapper)
        {
            this.courcesUsersRepository = courcesUsersRepository;
            this.courcesRepository = courcesRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task AddCource(CreateCourceViewModel model, GoUser user)
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

            var course = mapper.Map<Cource>(model);
            course.Creator = user;
            course.CreatorId = user.Id;
            course.Image = file;

            await this.courcesRepository.AddAsync(course);
            await this.courcesRepository.SaveChangesAsync();
        }

        public void AddResultToUsersCourses(UsersResultsViewModel model)
        {
            var userCouse = this.courcesUsersRepository.All()
                .FirstOrDefault(x => x.CourceId == model.CourceId && x.ParticipantId == model.ParticipantId);

            userCouse.StatusUser = model.Result;

            this.courcesUsersRepository.SaveChangesAsync();
        }

        public async Task AddUserToCource(string id, GoUser user)
        {
            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var userCource = new CourcesUsers
            {
                ParticipantId = user.Id,
                Participant = user,
                CourceId = course.Id,
                Cource = course
            };

            if (this.courcesUsersRepository.All().FirstOrDefault(x=>x.ParticipantId == user.Id && x.CourceId == course.Id) == null)
            {
                await this.courcesUsersRepository.AddAsync(userCource);
                await this.courcesUsersRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteCourse(string id)
        {
            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            this.courcesRepository.Delete(course);

            await this.courcesRepository.SaveChangesAsync();
        }

        public async Task EditCourse(EditCourseViewModel model)
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

            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == model.Id);

            course.Image = file;
            course.Title = model.Title;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.MaxCountParticipants = model.MaxCountParticipants;
            course.DurationOfDays = model.DurationOfDays;
            course.CountOfHours = model.CountOfHours;
            course.Category = model.Category;
            course.Status = model.Status;

            await this.courcesRepository.SaveChangesAsync();
        }

        public EditCourseViewModel FindCourse(string id)
        {
            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var courseModel = mapper.Map<EditCourseViewModel>(course);

            return courseModel;
        }

        public DeleteCourseViewModel FindCourseForDelete(string id)
        {
            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var courseModel = mapper.Map<DeleteCourseViewModel>(course);

            return courseModel;
        }

        public ICollection<CourceViewModel> GetAllCources()
        {
            var cources = this.courcesRepository.All().ToList();

            var courceModels = cources.Select(x => mapper.Map<CourceViewModel>(x)).ToList();

            return courceModels;
        }

        public ICollection<UsersResultsViewModel> GetAllParticipants(string id, GoUser user)
        {
            var users = this.courcesUsersRepository.All();

            var course = this.courcesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course.CreatorId != user.Id)
            {
                throw new ArgumentException("You not add results!");
            }

            var usersResult = users.Where(x => x.CourceId == id)
                .Select(x => mapper.Map<UsersResultsViewModel>(x)).ToList();

            usersResult.ForEach(x => x.Participant = mapper.Map<GoUserViewModel>(this.usersRepository.All()
                .FirstOrDefault(u => u.Id == x.ParticipantId)));

            return usersResult;
        }

        public CourseDetailsViewModel GetDetails(int? page, string id)
        {
            var cource = courcesRepository.All().FirstOrDefault(x => x.Id == id);

            var creatorr = this.usersRepository.All().FirstOrDefault(x => x.Id == cource.CreatorId);

            var creator = mapper.Map<GoUserViewModel>(creatorr);

            var participents = this.courcesUsersRepository.All()
                                            .Where(x => x.CourceId == id)
                                            .Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();

            var nextPage = page ?? 1;
            var pageParticipantsViewModels = participents.ToPagedList(nextPage, 8);

            var model = mapper.Map<CourseDetailsViewModel>(cource);
            model.Participants = pageParticipantsViewModels;
            model.FreeSeats = model.MaxCountParticipants - model.Participants.Count();

            return model;
        }

        public ICollection<CourceViewModel> GetMyCources(string id)
        {
            var courses = this.courcesUsersRepository.All().Where(x => x.ParticipantId == id)
                .Select(x => mapper.Map<CourceViewModel>(x.Cource)).ToList();

            return courses;
        }
    }
}
