using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class CommentsViewModelComparer : IEqualityComparer<CommentViewModel>
    {
        public bool Equals(CommentViewModel x, CommentViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id &&
                x.Content == y.Content &&
                x.ComentatorId == y.ComentatorId &&
                x.DestinationId == y.DestinationId;
        }

        public int GetHashCode(CommentViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.DestinationId.GetHashCode();
        }
    }
}
