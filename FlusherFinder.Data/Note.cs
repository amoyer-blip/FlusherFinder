using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }

        [ForeignKey(nameof(Rating))]
        public int RatingId { get; set; }

        public virtual Rating Rating { get; set; }

        [Required]
        public Guid SubId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
