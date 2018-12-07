using ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services.Contracts
{
    public interface IGamesService
    {
        void AddGame(CreateGameViewModel model);
    }
}
