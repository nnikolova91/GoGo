﻿using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Course
    {
        public Course()
        {
            this.Participants = new List<CoursesUsers>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MaxCountParticipants { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationOfDays { get; set; }

        public int CountOfHours { get; set; }

        public string CreatorId { get; set; }
        public GoUser Creator { get; set; }

        public Status Status { get; set; }

        public Category Category { get; set; }

        public ICollection<CoursesUsers> Participants { get; set; }
    }
}
