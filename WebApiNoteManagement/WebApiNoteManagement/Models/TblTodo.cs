using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Models
{
    public class TblTodo
    {
        [Key]
        public int TodoNoteId { get; set; }
        [Required, MaxLength(100)]
        public string Note { get; set; }
        [Required]
        public DateTime TodoDateTime { get; set; }
        [Required]
        public int IsComplete { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
