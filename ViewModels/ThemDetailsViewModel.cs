using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class ThemDetailsViewModel
    {
        public ThemDetailsViewModel()
        {
            this.Comments = new List<ThemCommentViewModel>();
        }
        
        public string Id { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
        
        public string AuthorId { get; set; }
        public string Author { get; set; }

        public string CurrentComment { get; set; }

        public ICollection<ThemCommentViewModel> Comments { get; set; }
    }
}
