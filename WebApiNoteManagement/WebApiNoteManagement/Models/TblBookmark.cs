using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.Models
{
    public class TblBookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        [Required, MaxLength(100)]
        public string SiteName { get; set; }
        [Required]
        public string BookmarkUrl { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
