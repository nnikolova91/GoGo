using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Game
    {
        public Game()
        {
            this.Levels = new List<GameLevelParticipant>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxPoints { get; set; }

        public Team Team { get; set; }

        public ICollection<GameLevelParticipant> Levels { get; set; }
    }
}
