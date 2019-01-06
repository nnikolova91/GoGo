using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;
using X.PagedList;

namespace ViewModels
{
    public class CourseDetailsViewModel
    {
        public CourseDetailsViewModel()
        {
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

        public GoUserViewModel Creator { get; set; }

        public IPagedList<GoUserViewModel> Participants { get; set; }

    }
}
