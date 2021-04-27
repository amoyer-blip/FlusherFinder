using FlusherFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class RatingEdit
    {
        public int RatingId { get; set; }
        public double CleanlinessRating { get; set; }
        public double AccessibilityRating { get; set; }
        public double AmenitiesRating { get; set; }
    }
}
