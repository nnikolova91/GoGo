﻿using GoGo.Models;
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

        ICollection<UsersResultsViewModel> GetAllParticipants(string id);

        void AddResultToUsersCourses(UsersResultsViewModel model);
    }
}
