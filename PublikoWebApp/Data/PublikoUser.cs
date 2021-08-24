using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoWebApp.Data
{
    public class PublikoUser : IdentityUser
    {
        [Required]
        public string WebSiteName { get; set; }
    }
}
