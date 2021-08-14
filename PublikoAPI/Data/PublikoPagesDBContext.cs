using Microsoft.EntityFrameworkCore;
using PublikoSharedLibrary.Models;

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
