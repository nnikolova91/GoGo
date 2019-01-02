using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class CreateLevelViewModel
    {
        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        public string Description { get; set; }
        
        public int Points { get; set; }

        public int NumberInGame { get; set; }

        public string GameId { get; set; }
        
    }
}
