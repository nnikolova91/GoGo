using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class CoursesUsers
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }

        public StatusParticitant StatusUser { get; set; }
    }
}
