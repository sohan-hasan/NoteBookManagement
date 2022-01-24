using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.ViewModels
{
    public class TblTodoViewModel
    {
        [Key]
        public int TodoNoteId { get; set; }
        [Required, MaxLength(100)]
        public string Note { get; set; }
        public DateTime TodoDateTime { get; set; }
        public string TodoDate { get; set; }
        public string TodoTime { get; set; }
        [Required]
        public int IsComplete { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
