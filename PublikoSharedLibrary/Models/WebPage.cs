using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublikoSharedLibrary.Models
{
    public class WebPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PageID { get; set; }
        [Required]
        public string PageTitle { get; set; }
#nullable enable
        public string? PageBody { get; set; }
#nullable disable
        [Required]
        public string UserID { get; set; }
    }
}
