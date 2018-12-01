using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.ViewModels
{
    public class CourceViewModel
    {
        public CourceViewModel()
        {
            this.Participants = new List<GoUserViewModel>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MaxCountParticipants { get; set; }

        public int FreeSeats { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationOfDays { get; set; }

        public int CountOfHours { get; set; }

        public Status Status { get; set; }

        public Category Category { get; set; }

        public GoUserViewModel Creator { get; set; }

        public ICollection<GoUserViewModel> Participants { get; set; }
    }
}
