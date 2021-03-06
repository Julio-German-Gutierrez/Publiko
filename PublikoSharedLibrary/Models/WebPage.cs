using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublikoSharedLibrary.Models
{
    public class WebPage
    {
#nullable enable
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? PageID { get; set; }
        public DateTime? PageDateCreated { get; set; }
        public DateTime? PageDateUpdated { get; set; }
#nullable disable
        [Required]
        public string PageTitle { get; set; }
        [Required]
        public string PageBody { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int PageOrder { get; set; } //Order from left to right in the Navbar.
    }
}
