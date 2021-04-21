using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class RatingCreate
    {
        [Required]
        public double CleanlinessRating { get; set; }

        [Required]
        public double AccessibilityRating { get; set; }

        [Required]
        public double AmenitiesRating { get; set; }
    }
}
