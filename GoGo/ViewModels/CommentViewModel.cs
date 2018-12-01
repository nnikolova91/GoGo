using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string ComentatorId { get; set; }
        public virtual GoUserViewModel Comentator { get; set; }

        public string DestinationId { get; set; }        
    }
}
