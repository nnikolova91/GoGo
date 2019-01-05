
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string ComentatorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public virtual GoUserViewModel Comentator { get; set; }

        public string DestinationId { get; set; }
    }
}
