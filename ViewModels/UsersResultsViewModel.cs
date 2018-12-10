﻿using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class UsersResultsViewModel
    {
        public string CourceId { get; set; }

        public string ParticipantId { get; set; }
        public GoUserViewModel Participant { get; set; }

        public StatusParticitant Result { get; set; }
    }
}