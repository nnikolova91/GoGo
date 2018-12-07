using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DestDetailsViewModel
    {
        public DestDetailsViewModel()
        {
            this.AllComments = new List<CommentViewModel>();
            this.ParticipantsKnowSomeone = new List<GoUserViewModel>();
            this.ParticipantsNotKnowAnyone = new List<GoUserViewModel>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public GoUserViewModel CurrentUser { get; set; }

        public string CurrentComment { get; set; }

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        public string Creator { get; set; }

        public Socialization Socialization { get; set; }

        public ICollection<CommentViewModel> AllComments { get; set; }

        public ICollection<StoryViewModel> Stories { get; set; }

        public ICollection<GoUserViewModel> ParticipantsKnowSomeone { get; set; }

        public ICollection<GoUserViewModel> ParticipantsNotKnowAnyone { get; set; }
    }
}
