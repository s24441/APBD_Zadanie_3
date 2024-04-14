using DataAccess.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Db
{
    public class PjatkDbContext : DbContext
    {
        public PjatkDbContext(DbContextOptions<PjatkDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
    }
}
