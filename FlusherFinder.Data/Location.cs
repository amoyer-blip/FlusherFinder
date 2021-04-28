using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Data
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public string LocationAddress { get; set; }

        [Required]
        public string IsFamilyFriendly { get; set; }

        [Required]
        public string IsTwentyFourHour { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public Guid CreatorId { get; set; }

        public bool IsRecommended
        {
            get
            {
                return Rating >= 4;
            }
        }

        public double Rating
        {
            get
            {
                double totalAveRating = 0;
                foreach (var rating in Ratings)
                {
                    totalAveRating += rating.AverageRating;
                }

                if (Ratings.Count > 0)
                {
                    return Math.Round(totalAveRating / Ratings.Count, 2);
                }
                else
                {
                    return 0;
                }

            }

        }
    }
}
