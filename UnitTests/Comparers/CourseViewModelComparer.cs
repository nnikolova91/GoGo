using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace UnitTests.Comparers
{
    class CourseViewModelComparer : IEqualityComparer<CourseViewModel>
    {
        public bool Equals(CourseViewModel x, CourseViewModel y)
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
                x.Image == x.Image &&
                x.Title == y.Title &&
                x.MaxCountParticipants == y.MaxCountParticipants &&
                x.DurationOfDays == y.DurationOfDays &&
                x.Status == y.Status && x.Category == y.Category;
        }

        public int GetHashCode(CourseViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
