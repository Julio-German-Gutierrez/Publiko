using Microsoft.EntityFrameworkCore;
using PublikoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublikoAPI.Data
{
    public class PublikoPagesDBContext : DbContext
    {
        public DbSet<Page> Pages { get; set; } 
        public PublikoPagesDBContext(DbContextOptions<PublikoPagesDBContext> options) : base(options)
        {

        }
    }
}
