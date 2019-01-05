
using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class GoUserViewModel
    {
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string FirstName { get; set; }

        public StatusParticitant StatusParticitant { get; set; }
    }
}
