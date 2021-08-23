using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublikoSharedLibrary.Models
{
    public class WebPage
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PageID { get; set; }
        [Required]
        public DateTime PageDateCreated { get; set; }
        [Required]
        public DateTime PageDateUpdated { get; set; }
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
