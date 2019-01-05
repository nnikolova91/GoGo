using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class ThemCommentViewModelComparer : IEqualityComparer<ThemCommentViewModel>
    {
        public bool Equals(ThemCommentViewModel x, ThemCommentViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Content == y.Content && x.AuthorId == y.AuthorId
                && x.Author == y.Author && x.Date == y.Date;
        }

        public int GetHashCode(ThemCommentViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
