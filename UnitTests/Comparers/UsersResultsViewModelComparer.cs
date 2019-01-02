using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class UsersResultsViewModelComparer : IEqualityComparer<UsersResultsViewModel>
    {
        public bool Equals(UsersResultsViewModel x, UsersResultsViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.CourceId == y.CourceId &&
                x.ParticipantId == y.ParticipantId &&
                x.Result == y.Result;    
        }

        public int GetHashCode(UsersResultsViewModel obj)
        {
            return obj.CourceId.GetHashCode() ^ obj.ParticipantId.GetHashCode();
        }
    }
}
