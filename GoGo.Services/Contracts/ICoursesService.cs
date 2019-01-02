using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using X.PagedList;

namespace GoGo.Services.Contracts
{
    public interface ICoursesService
    {
        Task AddCourse(CreateCourseViewModel model, GoUser user);

        ICollection<CourseViewModel> GetAllCourses();

        CourseDetailsViewModel GetDetails(int? page, string id);

        Task AddUserToCourse(string id, GoUser user);

        ICollection<UsersResultsViewModel> GetAllParticipants(string id, GoUser user);

        Task AddResultToUsersCourses(UsersResultsViewModel model, GoUser user);

        ICollection<CourseViewModel> GetMyCourses(string id);

        EditCourseViewModel FindCourse(string id);

        Task EditCourse(EditCourseViewModel model, GoUser user);

        DeleteCourseViewModel FindCourseForDelete(string id);

        Task DeleteCourse(string id, GoUser user);

        //IPagedList GetParticipentsToPagged(int? page, string id);
    }
}
