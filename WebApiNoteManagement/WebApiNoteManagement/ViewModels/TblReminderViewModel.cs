using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.ViewModels
{
    public class TblReminderViewModel
    {
        [Key]
        public int ReminderNoteId { get; set; }
        [Required, MaxLength(100)]
        public string Note { get; set; }
        public DateTime ReminderDateTime { get; set; }
        [Required]
        public string ReminderDate { get; set; }
        [Required]
        public string ReminderTime { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
