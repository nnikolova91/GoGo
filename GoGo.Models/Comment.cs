using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoGo.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string ComentatorId { get; set; }
        public virtual GoUser Comentator { get; set; }

        public string DestinationId { get; set; }
        public Destination Destination { get; set; }
    }
}
