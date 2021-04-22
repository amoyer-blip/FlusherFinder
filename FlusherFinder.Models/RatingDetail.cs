using FlusherFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class RatingDetail
    {
        public int RatingId { get; set; }
        public virtual Location Location { get; set; }

        public double CleanlinessRating { get; set; }

        public double AccessibilityRating { get; set; }

        public double AmenitiesRating { get; set; }
    }
}
