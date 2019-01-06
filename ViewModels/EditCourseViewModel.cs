using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class EditCourseViewModel
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Range((int)ModelsConstants.MinValue, ModelsConstants.MaxValue)]
        public int MaxCountParticipants { get; set; }

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
        
        [Required]
        public Status Status { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
