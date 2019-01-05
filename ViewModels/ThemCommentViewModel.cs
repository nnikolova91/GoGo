using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ThemCommentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }
        public string Author { get; set; }

        public DateTime Date { get; set; }

        public string ThemId { get; set; }
    }
}
