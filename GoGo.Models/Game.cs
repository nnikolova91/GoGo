using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoGo.Models
{
    public class Game
    {
        public Game()
        {
            this.Levels = new List<Level>();

            this.LevelsParticipants = new List<GameLevelParticipant>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxPoints { get; set; }

        public string CreatorId { get; set; }
        public GoUser Creator { get; set; }

        public ICollection<Level> Levels { get; set; }

        public ICollection<GameLevelParticipant> LevelsParticipants { get; set; }
    }
}
