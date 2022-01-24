using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNoteManagement.ViewModels
{
    public class TblBookmarkViewModel
    {
        [Key]
        public int BookmarkId { get; set; }
        [Required, MaxLength(50)]
        public string SiteName { get; set; }
        [Required, MaxLength(100)]
        public string BookmarkUrl { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
