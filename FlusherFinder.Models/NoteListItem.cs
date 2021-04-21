using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class NoteListItem
    {
        public int NoteId { get; set; }

        public int RatingId { get; set; }

        public string LocationName { get; set; }

        public string NoteTitle { get; set; }
    }
}
