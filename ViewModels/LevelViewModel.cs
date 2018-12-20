using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class LevelViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        public string Description { get; set; }

        public int Points { get; set; }

        public string GameId { get; set; }

        [Required]
        [Display(Name = " CorrespondingImage")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile CorrespondingImage { get; set; }
    }
}
