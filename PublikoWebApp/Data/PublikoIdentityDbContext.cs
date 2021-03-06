using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublikoWebApp.Data
{
    public class PublikoIdentityDbContext : IdentityDbContext<PublikoUser>
    {
        public PublikoIdentityDbContext(DbContextOptions<PublikoIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        } 
    }
}
