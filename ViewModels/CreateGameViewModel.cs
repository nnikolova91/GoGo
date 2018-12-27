using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CreateGameViewModel
    {
        public CreateGameViewModel()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int LevelsCount { get; set; }

        public string Level1Id { get; set; }
        public CreateLevelViewModel Level1 { get; set; }

        public string Level2Id { get; set; }
        public CreateLevelViewModel Level2 { get; set; }

        public string Level3Id { get; set; }
        public CreateLevelViewModel Level3 { get; set; }
    }
}
