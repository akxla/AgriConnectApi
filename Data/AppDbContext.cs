
using Microsoft.EntityFrameworkCore;
using AgriConnectApi.Models;

namespace AgriConnectApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AgricultureOfficer> AgricultureOfficers
            => Set<AgricultureOfficer>();

        public DbSet<Recommendation> Recommendations
    => Set<Recommendation>();
    }
}
