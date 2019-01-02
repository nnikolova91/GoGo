using GoGo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Models
{
    public class GoUser : IdentityUser
    {
        public GoUser() 
            : base()
        {
            this.Destinations = new List<DestinationsUsers>();
            this.Stories = new List<PeopleStories>();
            this.CreatedDestinations = new List<Destination>();
            this.CreatedStories = new List<Story>();
            this.Comments = new List<Comment>();
            this.Cources = new List<CourcesUsers>();
            this.Levels = new List<GameLevelParticipant>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Points { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        
        public byte[] Image { get; set; }

        
        public ICollection<GameLevelParticipant> Levels { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Destination> CreatedDestinations { get; set; }
        public ICollection<DestinationsUsers> Destinations { get; set; }
        public ICollection<Story> CreatedStories { get; set; }
        public ICollection<PeopleStories> Stories { get; set; }
        public ICollection<CourcesUsers> Cources { get; set; }
    }
}
