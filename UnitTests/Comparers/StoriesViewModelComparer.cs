using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class StoriesViewModelComparer : IEqualityComparer<StoryViewModel>
    {
        public bool Equals(StoryViewModel x, StoryViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.Title == y.Title && x.AuthorId == y.AuthorId 
                && x.DestinationId == y.DestinationId && x.Author == y.Author;
        }

        public int GetHashCode(StoryViewModel obj)
        {
            return obj.Id.GetHashCode() ^ obj.GetHashCode();
        }
    }
}
