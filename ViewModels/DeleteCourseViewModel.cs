using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DeleteCourseViewModel
    {
        public string Id { get; set; }
        
        public byte[] Image { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int MaxCountParticipants { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public int DurationOfDays { get; set; }
        
        public int CountOfHours { get; set; }
        
        public Status Status { get; set; }
        
        public Category Category { get; set; }
    }
}
