using Microsoft.EntityFrameworkCore;
using WestCoastEdu.Models;

namespace WestCoastEdu.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Status> Status { get; set; }
    }
}
