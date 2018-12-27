using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Story
    {
        public Story()
        {
            this.PeopleWhosLikeThis = new List<PeopleStories>();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }
        public GoUser Author { get; set; }
        
        public string DestinationId { get; set; }
        public Destination Destination { get; set; }
        
        public ICollection<PeopleStories> PeopleWhosLikeThis { get; set; }
    }
}
