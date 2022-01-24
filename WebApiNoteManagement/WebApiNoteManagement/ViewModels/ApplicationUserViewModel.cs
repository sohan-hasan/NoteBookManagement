using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.ViewModels
{
    public class ApplicationUserViewModel
    {
        [Key]
        public string UserId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string UserName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password{get; set;}
        public string Token { get; set; }
    }
}
