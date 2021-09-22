using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublikoSharedLibrary.Models
{
    public class WebPostEditIncomming
    {
        [Required]
        public string PostId { get; set; }
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostContent { get; set; }
        [Required]
        public string PostUserID { get; set; }
    }
}
