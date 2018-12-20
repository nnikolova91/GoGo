using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services.Contracts
{
    public interface ICourcesService
    {
        Task AddCource(CreateCourceViewModel model, GoUser user);

        ICollection<CourceViewModel> GetAllCources();

        CourceViewModel GetDetails(string id);

        Task AddUserToCource(string id, GoUser user);

        ICollection<UsersResultsViewModel> GetAllParticipants(string id, GoUser user);

        void AddResultToUsersCourses(UsersResultsViewModel model);

        ICollection<CourceViewModel> GetMyCources(string id);

        EditCourseViewModel FindCourse(string id);

        Task EditCourse(EditCourseViewModel model);

        DeleteCourseViewModel FindCourseForDelete(string id);

        Task DeleteCourse(string id);
    }
}
