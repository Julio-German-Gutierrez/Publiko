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
        public string PageTitle { get; set; }
        [Required]
        public string PageBody { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
