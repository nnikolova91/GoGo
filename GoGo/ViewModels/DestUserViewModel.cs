using GoGo.Models;
using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.ViewModels
{
    public class DestUserViewModel
    {
        public Socialization Socialization { get; set; }

        public string DestinationId { get; set; }
        public Destination Destination { get; set; }

        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }
    }
}
