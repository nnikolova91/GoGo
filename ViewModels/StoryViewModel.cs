using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Constants;

namespace ViewModels
{
    public class StoryViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(ModelsConstants.DescriptionMaxLength, MinimumLength = ModelsConstants.DescriptionMinLength)]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorId { get; set; }
        public string Author { get; set; }

        [Required]
        public string DestinationId { get; set; }

        public int PeopleWhosLikeThis { get; set; }
    }
}
