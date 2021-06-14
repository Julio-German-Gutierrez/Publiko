using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoAPI.Models
{
    public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PageID { get; set; }
        [Required]
        public string PageName { get; set; }
        [Required]
        public string PageContent { get; set; }
    }
}
