using System;
using System.Collections.Generic;
using System.Text;

namespace GoGo.Models
{
    public class Game
    {
        public Game()
        {
            this.Levels = new List<TeamLevelGame>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxPoints { get; set; }

        public Team Team { get; set; }

        public ICollection<TeamLevelGame> Levels { get; set; }
    }
}
