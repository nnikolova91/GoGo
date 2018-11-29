using GoGo.Models;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class DestinationsUsers : Controller
    {
        private IDestinationService destinationService;
        private UserManager<GoUser> _userManager;

        public DestinationsUsers(IDestinationService destinationService, UserManager<GoUser> userManager)
        {
            this.destinationService = destinationService;
            this._userManager = userManager;
        }

        
    }
}
