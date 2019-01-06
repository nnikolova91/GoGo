using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class LevelViewModelComparer : IEqualityComparer<LevelViewModel>
    {
        public bool Equals(LevelViewModel x, LevelViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Description == y.Description
                && x.NumberInGame == y.NumberInGame && x.Points == y.Points;
        }

        public int GetHashCode(LevelViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
