using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublikoSharedLibrary.Models
{
    public class WebPost
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PostID { get; set; }
        [Required]
        public DateTime PostDateCreated { get; set; }
        [Required]
        public DateTime PostDateModified { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
