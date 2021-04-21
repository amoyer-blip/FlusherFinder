﻿using System;
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
        [Range(0,10)]
        public double CleanlinessRating { get; set; }

        [Required, Range(0,10)]
        public double AccessibilityRating { get; set; }

        [Required, Range(0,10)]
        public double AmenitiesRating { get; set; }
    }
}
