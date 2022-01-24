using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.ViewModels
{
    public class TblRegularNoteViewModel
    {
        [Key]
        public int RegularNoteId { get; set; }
        [Required, MaxLength(100)]
        public string Note { get; set; }
        [Required]
        public DateTime NoteDateTime { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
