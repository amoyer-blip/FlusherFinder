using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class NoteCreate
    {
        [MinLength(1, ErrorMessage = "Please enter a title in the space. A minimum of 1 character is required. Thank you.")]
        [MaxLength(50, ErrorMessage = "Title is too long. A maximum of 50 characters. Thank you.")]
        public string NoteTitle { get; set; }

        public int RatingId { get; set; }
        [MaxLength(2000, ErrorMessage = "You have exceeded 2000 characters.")]
        public string NoteContent { get; set; }
    }
}
