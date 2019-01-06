using GoGo.Models.Enums;
using GoGo.Models;
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
            this.Comments = new List<Comment>();
            this.Participants = new List<DestinationsUsers>();
            this.Stories = new List<Story>();
        }
        [Key]
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        public string CreatorId { get; set; }
        public GoUser Creator { get; set; }
        
        public ICollection<Comment> Comments { get; set; }

        public ICollection<DestinationsUsers> Participants { get; set; }

        public ICollection<Story> Stories { get; set; }
    }
}
