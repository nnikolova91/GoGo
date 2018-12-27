using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class GameDetailsViewModel
    {
        public GameDetailsViewModel()
        {
            this.GameParticipantsLevel1 = new List<GameLevelParticipantViewModel>();

            this.GameParticipantsLevel2 = new List<GameLevelParticipantViewModel>();

            this.GameParticipantsLevel3 = new List<GameLevelParticipantViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GameId { get; set; }
        
        public string Level1Id { get; set; }
        public LevelViewModel Level1 { get; set; }

        public string Level2Id { get; set; }
        public LevelViewModel Level2 { get; set; }

        public string Level3Id { get; set; }
        public LevelViewModel Level3 { get; set; }

        public ICollection<GameLevelParticipantViewModel> GameParticipantsLevel1 { get; set; }

        public ICollection<GameLevelParticipantViewModel> GameParticipantsLevel2 { get; set; }

        public ICollection<GameLevelParticipantViewModel> GameParticipantsLevel3 { get; set; }
    }
}
