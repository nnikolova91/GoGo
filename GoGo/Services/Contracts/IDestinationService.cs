using GoGo.Models;
using GoGo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services.Contracts
{
    public interface IDestinationService
    {
        void AddDestination(CreateDestinationViewModel model, GoUser user);

        ICollection<DestViewModel> GetAllDestinations();

        DestDetailsViewModel GetDetails(string id);

        void AddUserToDestination(GoUser user, string id);

        ICollection<GoUserViewModel> Socializer(string socialization,string id);
    }
}
