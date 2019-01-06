using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoGo.Models
{
    public class ThemComment
    {
        public string Id { get; set; }
        
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string ThemId { get; set; }
        public Theme Them { get; set; }

        public string AuthorId { get; set; }
        public GoUser Author { get; set; }
    }
}
