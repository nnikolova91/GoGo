using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class GoUserViewModelComparer : IEqualityComparer<GoUserViewModel>
    {
        public bool Equals(GoUserViewModel x, GoUserViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.FirstName == y.FirstName/* && x.Image == y.Image*/;
        }

        public int GetHashCode(GoUserViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
