using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class GameLevelParticipant
    {
        public string ParticipantId { get; set; }
        public GoUser Participant { get; set; }

        public string LevelId { get; set; }
        public Level Level { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }

        public byte[] CorrespondingImage { get; set; }

        public StatusLevel StatusLevel { get; set; }
    }
}
