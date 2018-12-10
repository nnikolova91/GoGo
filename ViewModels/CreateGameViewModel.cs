using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CreateGameViewModel 
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int LevelsCount { get; set; }

        public ICollection<LevelViewModel> Levels { get; set; }
    }
}
