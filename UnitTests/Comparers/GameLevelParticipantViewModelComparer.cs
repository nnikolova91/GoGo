using GoGo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class GameLevelParticipantViewModelComparer : IEqualityComparer<GameLevelParticipantViewModel>
    {
        public bool Equals(GameLevelParticipantViewModel x, GameLevelParticipantViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.GameId == y.GameId && x.LevelId == y.LevelId && x.ParticipantId == y.ParticipantId
                && x.StatusLevel == y.StatusLevel && x.Participant == y.Participant;
        }

        public int GetHashCode(GameLevelParticipantViewModel obj)
        {
            return obj.GameId.GetHashCode() ^ obj.LevelId.GetHashCode() ^ obj.ParticipantId.GetHashCode();
        }
    }
}
