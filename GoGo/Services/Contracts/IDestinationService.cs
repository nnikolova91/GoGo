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

        DestDetailsViewModel GetDetails(string id, GoUser user);

        DestUserViewModel AddUserToDestination(GoUser user, string id);

        void AddSocialization(GoUser user, string id, string socialization);

        ICollection<GoUserViewModel> AllUsersFodSocialization(GoUser user, string id, string socialization);
    }
}
