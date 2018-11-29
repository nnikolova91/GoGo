using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class DestinationsUsers
    {
        //public string Id { get; set; }
        public Socialization Socialization { get; set; }

        public string DestinationId { get; set; }
        public Destination Destination { get; set; }

        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }
    }
}
