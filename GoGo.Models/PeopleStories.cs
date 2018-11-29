using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class PeopleStories
    {
        public string StoryId { get; set; }
        public Story Story { get; set; }

        public string UserId { get; set; }
        public GoUser User { get; set; }
    }
}
