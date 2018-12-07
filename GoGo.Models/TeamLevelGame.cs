using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class TeamLevelGame
    {
        public string TeamId { get; set; }
        public Team Team { get; set; }

        public string LevelId { get; set; }
        public Level Level { get; set; }

        public string GameId { get; set; }
        public Game Game { get; set; }

        public byte[] Image { get; set; }

        public bool IsPassed { get; set; }
    }
}
