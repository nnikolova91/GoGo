using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class LevelViewModel
    {
        public string Id { get; set; }
        
        public byte[] Image { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public int Points { get; set; }

        public string GameId { get; set; }
        
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile CorrespondingImage { get; set; }

        [Required]
        public int NumberInGame { get; set; }
    }
}
