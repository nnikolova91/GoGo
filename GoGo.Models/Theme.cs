using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Theme
    {
        public Theme()
        {
            this.ThemComments = new List<ThemComment>();
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }
        public GoUser Author { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public ICollection<ThemComment> ThemComments { get; set; }
    }
}
