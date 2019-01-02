using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class DestViewModelComparer : IEqualityComparer<DestViewModel>
    {
        public bool Equals(DestViewModel x, DestViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode(DestViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
