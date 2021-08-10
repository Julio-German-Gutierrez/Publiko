using Microsoft.EntityFrameworkCore;
using PublikoSharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoAPI.Data
{
    public class PublikoPagesDBContext : DbContext
    {
        public DbSet<WebPage> Pages { get; set; }
        public DbSet<WebPost> Posts { get; set; }
        public PublikoPagesDBContext(DbContextOptions<PublikoPagesDBContext> options) : base(options)
        {

        }
    }
}
