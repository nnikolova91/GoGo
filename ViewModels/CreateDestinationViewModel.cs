
using GoGo.Models;
using GoGo.Models.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Attributes;
using ViewModels.Constants;

namespace ViewModels
{
    public class CreateDestinationViewModel 
    {
        public const string EndDateToJoinPropertyName = "EndDateToJoin";
        public const string StartDatePropertyName = "StartDate";
        public const string EndDateToJoinBeforeStartDateErrorMessage = "End date to join must by before Start date";
        public const string StartDateBeforeEndDateErrorMessage = "Start date must by before End date";

        [Required]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        [Required]
        public LevelOfDifficulty Level { get; set; }

        [Required]
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

        [Required]
        public DateTime EndDateToJoin { get; set; }
    }
}
