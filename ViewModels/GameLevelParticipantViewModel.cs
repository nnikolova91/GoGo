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
        //public Level Level { get; set; }

        public string GameId { get; set; }
        //public Game Game { get; set; }

        public byte[] CorrespondingImage { get; set; }

        public bool IsPassed { get; set; }
    }
}
