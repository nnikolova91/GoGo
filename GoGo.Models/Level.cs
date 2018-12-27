using GoGo.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Level
    {


        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }

        public int NumberInGame { get; set; }

        public int Points { get; set; }

        public ICollection<GameLevelParticipant> Participants { get; set; }
    }
}
