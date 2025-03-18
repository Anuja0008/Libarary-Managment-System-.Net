using Microsoft.EntityFrameworkCore;
using YourProject.Models; // Adjust according to your project namespace

namespace YourProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for the models
        public DbSet<User> Users { get; set; }  // Users table
        public DbSet<Member> Members { get; set; }  // Members table
    }
}
