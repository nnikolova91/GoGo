using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services.Contracts
{
    public interface IThemService
    {
        Task AddThem(CreateThemViewModel model, GoUser user);

        Task AddCommentToThem(string themId, string currentComment, GoUser user);

        ICollection<ThemDetailsViewModel> GetAllThems();

        ThemDetailsViewModel GetDetails(string themId, GoUser user);
    }
}
