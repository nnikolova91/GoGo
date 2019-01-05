using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class ThemDetailsViewModelComparer : IEqualityComparer<ThemDetailsViewModel>
    {
        public bool Equals(ThemDetailsViewModel x, ThemDetailsViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Description == y.Description && x.AuthorId == y.AuthorId
                && x.Author == y.Author && x.Date == y.Date && x.Comments.Count() == y.Comments.Count();
        }

        public int GetHashCode(ThemDetailsViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
