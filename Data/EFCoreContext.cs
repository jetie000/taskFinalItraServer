using finalTaskItra.Models;
using Microsoft.EntityFrameworkCore;

namespace finalTaskItra.Data
{
    public class EFCoreContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public EFCoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> users { get; set; } = null!;
        public DbSet<Collection> collections { get; set; } = null!;
        public DbSet<Item> items { get; set; } = null!;
        public DbSet<Comment> comments { get; set; } = null!;
        public DbSet<Reaction> likes { get; set; } = null!;
        public DbSet<ItemFields> itemFields { get; set; } = null!;
        public DbSet<Tag> tags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AppCon"));
        }
    }
}
