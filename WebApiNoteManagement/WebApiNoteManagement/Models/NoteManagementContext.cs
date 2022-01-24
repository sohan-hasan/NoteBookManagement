using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Models
{
    public class NoteManagementContext : IdentityDbContext<ApplicationUser>
    {
        public NoteManagementContext(DbContextOptions<NoteManagementContext> options) : base(options) { }

        public virtual DbSet<TblRegularNote> TblRegularNotes { get; set; }
        public virtual DbSet<TblBookmark> TblBookmarks { get; set; }
        public virtual DbSet<TblTodo> TblTodos { get; set; }
        public virtual DbSet<TblReminder> TblReminders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
