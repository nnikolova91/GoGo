using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.ViewModels
{
    public class StoryViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }
        public GoUserViewModel Author { get; set; }

        public string DestinationId { get; set; }

        public int PeopleWhosLikeThis { get; set; }
    }
}
