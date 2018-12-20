using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class EditCourseViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(700, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range((int)1, int.MaxValue)]
        public int MaxCountParticipants { get; set; }

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
        
        [Required]
        public Status Status { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
