using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class LocationEdit
    {
        [Required]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string IsFamilyFriendly { get; set; }
        public string IsTwentyFourHour { get; set; }
    }
}
