﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Controllers
{
    public class ChatsController : Controller
    {
        [Authorize]
        public IActionResult Chat()
        {

            return View();
        }
    }
}
