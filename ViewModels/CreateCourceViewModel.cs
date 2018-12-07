using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class CreateCourceViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxCountParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationOfDays { get; set; }
        public int CountOfHours { get; set; }
        public GoUserViewModel Creator { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }
    }
}
