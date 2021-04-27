using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Models
{
    public class NoteEdit
    {
        [Required]
        public int NoteId { get; set; }

        public string NoteTitle { get; set; }

        public string NoteContent { get; set; }
    }
}
