using FlusherFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class LocationDetails
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public string LocationAddress { get; set; }

        public string IsFamilyFriendly { get; set; }

        public string IsTwentyFourHour { get; set; }

        public bool IsRecommended { get; set; }

        public double Rating { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
