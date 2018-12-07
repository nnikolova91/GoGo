using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class StoryViewModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string DestinationId { get; set; }
        public int PeopleWhosLikeThis { get; set; }
    }
}
