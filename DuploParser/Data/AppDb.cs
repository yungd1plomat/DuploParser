using DuploParser.Models;
using Microsoft.EntityFrameworkCore;

namespace DuploParser.Data
{
    public class AppDb : DbContext
    {
        public DbSet<Filter> Filters { get; set; }

        public AppDb(DbContextOptions<AppDb> options) 
            : base(options) 
        {
            Database.EnsureCreated();
        }

    }
}
