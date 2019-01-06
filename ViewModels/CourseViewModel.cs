
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ViewModels.Constants;

namespace ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            this.Participants = new List<GoUserViewModel>();
        }
        public string Id { get; set; }

        public byte[] Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range((int)ModelsConstants.MinValue, ModelsConstants.MaxValue)]
        public int MaxCountParticipants { get; set; }
        
        public int FreeSeats { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CompareWithToday(ErrorMessage = ModelsConstants.PassedDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [Range((int)ModelsConstants.MinValue, ModelsConstants.MaxValue)]
        public int DurationOfDays { get; set; }

        [Required]
        [Range((int)ModelsConstants.MinValue, ModelsConstants.MaxValue)]
        public int CountOfHours { get; set; }

        public Status Status { get; set; }

        public Category Category { get; set; }

        public string CreatorId { get; set; }
        public GoUserViewModel Creator { get; set; }

        public ICollection<GoUserViewModel> Participants { get; set; }
    }
    
}
