using GoGo.Models;
using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services.Contracts
{
    public interface ICommentsService
    {
        Task AddComment(string comment, string destinationId, GoUser user);
    }
}
