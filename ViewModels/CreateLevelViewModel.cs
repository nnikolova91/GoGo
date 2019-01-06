using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class CreateLevelViewModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Range((int)1, ModelsConstants.MaxValue)]
        public int Points { get; set; }

        [Range((int)1, 3)]
        public int NumberInGame { get; set; }

        public string GameId { get; set; }
        
    }
}
