using GoGo.Models.Enums;
using GoGo.Models.Photos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoGo.Models
{
    public class Destination
    {
        public Destination()
        {
            this.Acsesoaries = new List<Acsesoar>();
            this.Comments = new List<Comment>();
            this.Participants = new List<DestinationsUsers>();
            this.Photos = new List<DestinationPhoto>();
            this.Stories = new List<Story>();
        }
        [Key]
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public Level Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        public string CreatorId { get; set; }
        public GoUser Creator { get; set; }

        public ICollection<Acsesoar> Acsesoaries { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<DestinationsUsers> Participants { get; set; }

        public ICollection<DestinationPhoto> Photos { get; set; }

        public ICollection<Story> Stories { get; set; }
    }
}
