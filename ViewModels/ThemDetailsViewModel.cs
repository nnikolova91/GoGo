using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ThemDetailsViewModel
    {
        public ThemDetailsViewModel()
        {
            this.Comments = new List<ThemCommentViewModel>();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }
        public string Author { get; set; }

        public string CurrentComment { get; set; }

        public ICollection<ThemCommentViewModel> Comments { get; set; }
    }
}
