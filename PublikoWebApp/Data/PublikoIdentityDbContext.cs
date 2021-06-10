using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublikoWebApp.Data
{
    public class PublikoIdentityDbContext : IdentityDbContext
    {
        public PublikoIdentityDbContext(DbContextOptions<PublikoIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
