using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class DestUserViewModelComparer : IEqualityComparer<DestUserViewModel>
    {
        public bool Equals(DestUserViewModel x, DestUserViewModel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.DestinationId == y.DestinationId &&
                x.ParticipantId == y.ParticipantId;
        }

        public int GetHashCode(DestUserViewModel obj)
        {
            return obj.DestinationId.GetHashCode() ^
                obj.ParticipantId.GetHashCode();
        }
    }
}
