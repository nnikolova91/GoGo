
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CourceViewModel //: IMapFrom<Cource>//, IHaveCustomMappings
    {
        public CourceViewModel()
        {
            this.Participants = new List<GoUserViewModel>();
        }
        public string Id { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 700)]
        public string Description { get; set; }

        [Required]
        [Range((int)1, int.MaxValue)]
        public int MaxCountParticipants { get; set; }
        
        public int FreeSeats { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CompareWithToday(ErrorMessage = "This date is passed enter new date:)")]
        public DateTime StartDate { get; set; }

        [Required]
        [Range((int)1, int.MaxValue)]
        public int DurationOfDays { get; set; }

        [Required]
        [Range((int)1, int.MaxValue)]
        public int CountOfHours { get; set; }

        public Status Status { get; set; }

        public Category Category { get; set; }

        public GoUserViewModel Creator { get; set; }

        public ICollection<GoUserViewModel> Participants { get; set; }

        
    }
    
}
