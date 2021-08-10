using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublikoSharedLibrary.Models
{
    public class WebPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PostID { get; set; }
        [Required]
        public string PostTitle { get; set; }
#nullable enable
        public string? PostContent { get; set; }
#nullable disable
        [Required]
        public string UserID { get; set; }
    }
}
