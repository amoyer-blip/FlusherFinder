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

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating { get; set; }

        public double IsRecommended { get; set; }

        public double IsFamilyFriendly { get; set; }

        public double IsTwentyFourHour { get; set; }
    }
}
