using finalTaskItra.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace finalTaskItra.Data
{
    public class EFCoreContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public EFCoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(user => user.collections)
                .WithOne(collection => collection.user)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<MyCollection>()
                .HasMany(collection => collection.collectionFields)
                .WithOne(collectionField => collectionField.myCollection)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<MyCollection>()
                .HasMany(collection => collection.items)
                .WithOne(item => item.myCollection)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Item>()
                .HasMany(item => item.fields)
                .WithOne(field => field.item)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Item>()
                .HasMany(item => item.likes)
                .WithOne(like => like.item)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Item>()
                .HasMany(item => item.tags)
                .WithOne(tag => tag.item)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<User> users { get; set; } = null!;
        public DbSet<MyCollection> collections { get; set; } = null!;
        public DbSet<CollectionFields> collectionFields { get; set; } = null!;
        public DbSet<Item> items { get; set; } = null!;
        public DbSet<ItemFields> itemFields { get; set; } = null!;
        public DbSet<Comment> comments { get; set; } = null!;
        public DbSet<Reaction> likes { get; set; } = null!;
        public DbSet<Tag> tags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AppCon"));
        }
    }
}
