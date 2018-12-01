using GoGo.Models;
using GoGo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services.Contracts
{
    public interface ICommentsService
    {
        void AddComment(string comment, string destinationId, GoUser user);
    }
}
