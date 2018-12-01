using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class CourcesUsers
    {
        public string CourceId { get; set; }
        public Cource Cource { get; set; }

        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }

        public StatusParticitant StatusUser { get; set; }
    }
}
