using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class DestDetailsViewModelComparer : IEqualityComparer<DestDetailsViewModel>
    {
        public bool Equals(DestDetailsViewModel x, DestDetailsViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Description == y.Description && x.AllComments.ToList().Count() == y.AllComments.ToList().Count();
        }

        public int GetHashCode(DestDetailsViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
