using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class GameDetailsViewModelComparer : IEqualityComparer<GameDetailsViewModel>
    {
        public bool Equals(GameDetailsViewModel x, GameDetailsViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Name == y.Name && x.Description == y.Description && x.Creator == y.Creator &&
                x.GameParticipantsLevel1.Count() == y.GameParticipantsLevel1.Count() &&
                x.GameParticipantsLevel2.Count() == y.GameParticipantsLevel2.Count() &&
                x.GameParticipantsLevel3.Count() == y.GameParticipantsLevel3.Count();
        }

        public int GetHashCode(GameDetailsViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
