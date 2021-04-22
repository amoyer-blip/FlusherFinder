using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class LocationCreate
    {
        [Required]
        public int LocationId { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public string LocationAddress { get; set; }
        
        public string IsFamilyFriendly { get; set; }
     
        public string IsTwentyFourHour { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
