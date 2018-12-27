
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CreateStoryViewModel
    {
        public string Content { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string DestinationId { get; set; }
    }
}
