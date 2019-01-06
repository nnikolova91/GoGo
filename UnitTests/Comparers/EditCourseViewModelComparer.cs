using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class EditCourseViewModelComparer : IEqualityComparer<EditCourseViewModel>
    {
        public bool Equals(EditCourseViewModel x, EditCourseViewModel y)
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
                x.Description == y.Description && x.Status == y.Status && x.Category == y.Category
                && x.StartDate == y.StartDate && x.Title == y.Title && x.MaxCountParticipants == y.MaxCountParticipants &&
                x.DurationOfDays == y.DurationOfDays; ;
        }

        public int GetHashCode(EditCourseViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
