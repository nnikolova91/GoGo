using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class UsersResultsViewModel
    {
        [Required]
        public string CourseId { get; set; }
        public CourseViewModel Course { get; set; }

        public string ParticipantId { get; set; }
        public GoUserViewModel Participant { get; set; }

        public StatusParticitant Result { get; set; }
    }
}
