using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : base(userName) { }
        public ApplicationUser(string userId, string userName, string name, DateTime dateOfBirth, string email) : base(userName)
        {

            this.Id = userId;
            base.UserName = userName;
            this.Name = name;
            this.DateOfBirth = dateOfBirth;
            this.Email = email;
        }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public ICollection<TblRegularNote> TblRegularNotes { get; set; }
        public ICollection<TblBookmark> TblBookmarks { get; set; }
        public ICollection<TblTodo> TblTodos { get; set; }
        public ICollection<TblReminder> TblReminders { get; set; }

    }
}
