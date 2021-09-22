using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublikoSharedLibrary.Models
{
    public class WebPageEditIncomming
    {
        [Required]
        public string PageId { get; set; }
        [Required]
        public string PageTitle { get; set; }
        [Required]
        public string PageBody { get; set; }
        [Required]
        public string PageUserID { get; set; }
        [Required]
        public int PageOrder { get; set; }
    }
}
