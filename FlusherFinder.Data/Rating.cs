using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Data
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        [Required]
        public Guid CreatorId { get; set; }
       
        [Required]
        [Range(0,2)]
        public double CleanlinessRating { get; set; }

        [Required, Range(0,10)]
        //For Accessibility & Amenities ratings:
        //Range will change depending on the number of factors we will be looking at.
        //IE, right now we're set up to look at 10 different amenities. Each amenity will get a "1"
        //if it is present, otherwise it will get a "0". 
        public double AccessibilityRating { get; set; }

        [Required, Range(0,10)]
        public double AmenitiesRating { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public double AverageRating
        {
            get
            {
                var totalScore = CleanlinessRating + AmenitiesRating + AccessibilityRating;
                return Math.Round(totalScore / 3, 2);
            }
        }
    }
}
