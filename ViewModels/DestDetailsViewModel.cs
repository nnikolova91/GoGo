
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Attributes;
using ViewModels.Constants;

namespace ViewModels
{
    public class DestDetailsViewModel 
    {
        public const string EndDateToJoinPropertyName = "EndDateToJoin";
        public const string StartDatePropertyName = "StartDate";
        public const string EndDateToJoinBeforeStartDateErrorMessage = "End date to join must by before Start date";
        public const string StartDateBeforeEndDateErrorMessage = "Start date must by before End date";

        public DestDetailsViewModel()
        {
            this.AllComments = new List<CommentViewModel>();
            this.ParticipantsKnowSomeone = new List<GoUserViewModel>();
            this.ParticipantsNotKnowAnyone = new List<GoUserViewModel>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; }

        public string CurrentComment { get; set; }

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [StartDateBaforeEndDate(EndDateToJoinPropertyName, ErrorMessage = EndDateToJoinBeforeStartDateErrorMessage)]
        [CompareWithToday(ErrorMessage = ModelsConstants.PassedDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [StartDateBaforeEndDate(StartDatePropertyName, ErrorMessage = StartDateBeforeEndDateErrorMessage)]
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
