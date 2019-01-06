using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class EditDestinationViewModelComparer : IEqualityComparer<EditDestinationViewModel>
    {
        public bool Equals(EditDestinationViewModel x, EditDestinationViewModel y)
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
                x.Description == y.Description && x.Level == y.Level && x.Naame == y.Naame
                && x.StartDate == y.StartDate && x.EndDateToJoin == y.EndDateToJoin && x.EndDate == y.EndDate;
        }

        public int GetHashCode(EditDestinationViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
