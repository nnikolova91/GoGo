using GoGo.Models;
using GoGo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services.Contracts
{
    public interface IDestinationService
    {
        Task AddDestination(CreateDestinationViewModel model, GoUser user);

        ICollection<DestViewModel> GetAllDestinations();

        DestDetailsViewModel GetDetails(string id, GoUser user); //GoUser user);

        Task<DestUserViewModel> AddUserToDestination(GoUser user, string id);

        Task AddSocialization(GoUser user, string id, string socialization);

        //ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization);

        ICollection<DestViewModel> FindMyDestinations(GoUser user);

        EditDestinationViewModel FindEditDestination(string id, GoUser user);

        Task EditDestination(EditDestinationViewModel model);

        DestViewModel FindToDeleteDestination(string id, GoUser user);

        Task DeleteDestinationsUsers(string id);

        Task DeleteComments(string id);

        Task DeleteFromStories(string id);

        Task DeleteDestination(string id);
    }
}
