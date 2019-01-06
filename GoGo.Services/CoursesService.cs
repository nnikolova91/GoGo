using AutoMapper;
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
    public class CoursesService : ICoursesService
    {
        public const string CourseNotExistException = "This course not exist!";
        public const string UserCourseNotExistException = "This userCourse not exist!";
        public const string YouCanNotAddResultsException = "You can not add results!";
        public const string YouCanNotEditException = "You can not edit this page";
        public const string YouCanNotDeleteException = "You can not delete this page";
        public const string CourseIsNotFinished = "Course is not finished!";
        
        private readonly IRepository<CoursesUsers> coursesUsersRepository;
        private readonly IRepository<Course> coursesRepository;
        private readonly IRepository<GoUser> usersRepository;
        private readonly IMapper mapper;

        public CoursesService(IRepository<CoursesUsers> coursesUsersRepository,
                                IRepository<Course> coursesRepository,
                                IRepository<GoUser> usersRepository,
                                IMapper mapper)
        {
            this.coursesUsersRepository = coursesUsersRepository;
            this.coursesRepository = coursesRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task AddCourse(CreateCourseViewModel model, GoUser user)
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

            var course = mapper.Map<Course>(model);
            course.Creator = user;
            course.CreatorId = user.Id;
            course.Image = file;

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task AddResultToUsersCourses(UsersResultsViewModel model, GoUser user)
        {
            var userCouse = this.coursesUsersRepository.All()
                .FirstOrDefault(x => x.CourseId == model.CourseId && x.ParticipantId == model.ParticipantId);

            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == model.CourseId);

            if (userCouse == null)
            {
                throw new ArgumentException(UserCourseNotExistException);
            }
            if (course.CreatorId != user.Id)
            {
                throw new ArgumentException(YouCanNotAddResultsException);
            }

            userCouse.StatusUser = model.Result;

            if (course.StartDate.AddDays(course.DurationOfDays) > DateTime.Now)
            {
                throw new ArgumentException(CourseIsNotFinished);
            }

            await this.coursesUsersRepository.SaveChangesAsync();
        }

        public async Task AddUserToCourse(string id, GoUser user)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            var userCourse = new CoursesUsers
            {
                ParticipantId = user.Id,
                Participant = user,
                CourseId = course.Id,
                Course = course
            };

            if (this.coursesUsersRepository.All().FirstOrDefault(x => x.ParticipantId == user.Id && x.CourseId == course.Id) == null
                && course.MaxCountParticipants > this.coursesUsersRepository.All().Where(x => x.CourseId == course.Id).Count()
                && DateTime.Now < course.StartDate)
            {
                await this.coursesUsersRepository.AddAsync(userCourse);
                await this.coursesUsersRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteCourse(string id, GoUser user)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course != null && course.CreatorId == user.Id)
            {
                this.coursesRepository.Delete(course);

                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task EditCourse(EditCourseViewModel model, GoUser user)
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

            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == model.Id);

            if (course != null && course.CreatorId == user.Id)
            {
                course.Image = file;
                course.Title = model.Title;
                course.Description = model.Description;
                course.StartDate = model.StartDate;
                course.MaxCountParticipants = model.MaxCountParticipants;
                course.DurationOfDays = model.DurationOfDays;
                course.CountOfHours = model.CountOfHours;
                course.Category = model.Category;
                course.Status = model.Status;

                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public EditCourseViewModel FindCourse(string id)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new ArgumentException(YouCanNotEditException);
            }

            var courseModel = mapper.Map<EditCourseViewModel>(course);

            return courseModel;
        }

        public DeleteCourseViewModel FindCourseForDelete(string id)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new ArgumentException(YouCanNotDeleteException);
            }

            var courseModel = mapper.Map<DeleteCourseViewModel>(course);

            return courseModel;
        }

        public ICollection<CourseViewModel> GetAllCourses()
        {
            var courses = this.coursesRepository.All().ToList();

            var courseModels = courses.Select(x => mapper.Map<CourseViewModel>(x)).ToList();

            return courseModels;
        }

        public ICollection<UsersResultsViewModel> GetAllParticipants(string id, GoUser user)
        {
            var users = this.coursesUsersRepository.All();

            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course.CreatorId != user.Id)
            {
                throw new ArgumentException(YouCanNotAddResultsException);
            }

            var usersResult = users.Where(x => x.CourseId == id)
                .Select(x => mapper.Map<UsersResultsViewModel>(x)).ToList();

            usersResult.ForEach(x => x.Course = mapper.Map<CourseViewModel>(this.coursesRepository.All()
                .FirstOrDefault(c => c.Id == x.CourseId)));

            usersResult.ForEach(x => x.Participant = mapper.Map<GoUserViewModel>(this.usersRepository.All()
                .FirstOrDefault(u => u.Id == x.ParticipantId)));

            return usersResult;
        }

        public CourseDetailsViewModel GetDetails(int? page, string id)
        {
            var course = coursesRepository.All().FirstOrDefault(x => x.Id == id);

            if (course == null)
            {
                throw new ArgumentException(CourseNotExistException);
            }

            var creatorr = this.usersRepository.All().FirstOrDefault(x => x.Id == course.CreatorId);

            var creator = mapper.Map<GoUserViewModel>(creatorr);

            var participents = this.coursesUsersRepository.All()
                                            .Where(x => x.CourseId == id).Select(x => x).ToList();

            participents.ForEach(x => x.Participant = this.usersRepository.All().FirstOrDefault(u => u.Id == x.ParticipantId));

            var part = participents.Select(x => mapper.Map<GoUserViewModel>(x.Participant)).ToList();

            part.ForEach(x => x.StatusParticitant = this.coursesUsersRepository
                                            .All().FirstOrDefault(c => c.CourseId == id && c.ParticipantId == x.Id).StatusUser);

            var nextPage = page ?? 1;
            var pageParticipantsViewModels = part.ToPagedList(nextPage, 8);

            var model = mapper.Map<CourseDetailsViewModel>(course);

            model.Participants = pageParticipantsViewModels;
            model.FreeSeats = model.MaxCountParticipants - participents.Count();
            model.Creator = creator;

            return model;
        }

        public ICollection<CourseViewModel> GetMyCourses(string id)
        {
            var courses = this.coursesUsersRepository.All().Where(x => x.ParticipantId == id)
                .Select(x => this.coursesRepository.All().FirstOrDefault(c => c.Id == x.CourseId)).ToList();

            var coursess = courses.Select(x => mapper.Map<CourseViewModel>(x)).ToList();

            return coursess;
        }
    }
}
