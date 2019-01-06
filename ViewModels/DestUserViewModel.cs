
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class DestUserViewModel
    {
        public Socialization Socialization { get; set; }

        [Required]
        public string DestinationId { get; set; }
        public Destination Destination { get; set; }

        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }
    }
}
