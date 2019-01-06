using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class GameLevelParticipantViewModel
    {
        public string ParticipantId { get; set; }
        public string Participant { get; set; }

        public string LevelId { get; set; }

        public string GameId { get; set; }

        public byte[] CorrespondingImage { get; set; }

        public StatusLevel StatusLevel { get; set; }
    }
}
