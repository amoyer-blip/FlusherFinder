using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class NoteDetail
    {
        public int NoteId { get; set; }

        public int? RatingId { get; set; }

        public string NoteTitle { get; set; }

        public string NoteContent { get; set; }

        [Display(Name = "Note Created On")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Note Modified On")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
