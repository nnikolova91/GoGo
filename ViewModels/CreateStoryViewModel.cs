
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class CreateStoryViewModel
    {
        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string DestinationId { get; set; }
    }
}
