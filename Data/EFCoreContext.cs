using finalTaskItra.Controllers;
using finalTaskItra.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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

            //if (this.users.FirstOrDefault(user => user.role == 1) is null)
            //{
            //    User user = new User();
            //    user.email = "admin@collections.com";
            //    user.access = true;
            //    user.role = 1;
            //    user.id = 0;
            //    user.fullName = "Admin Adminovich";
            //    user.loginDate = DateTime.Now;
            //    user.joinDate = DateTime.Now;
            //    user.isOnline = false;
            //    var claims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name, user.fullName),
            //            new Claim(ClaimTypes.Role, Convert.ToString(user.role)),
            //            new Claim(ClaimTypes.Email, user.email),
            //        };
            //    // создаем JWT-токен
            //    var jwt = new JwtSecurityToken(
            //            claims: claims,
            //            signingCredentials: new SigningCredentials(new AuthOptions(_configuration).GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            //            );

            //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            //    user.accessToken = encodedJwt; 
            //    string saltedPassword = "Admin12345" + _configuration["Salt"];
            //    for (int i = 0; i < Convert.ToInt32(_configuration["Iterations"]); i++)
            //    {
            //        saltedPassword = HashWithSHA256(saltedPassword);
            //    }
            //    modelBuilder.Entity<User>().HasData(user);
            //    this.SaveChanges();
            //}
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
        //public static string HashWithSHA256(string value)
        //{
        //    using var hash = SHA256.Create();
        //    var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
        //    return Convert.ToHexString(byteArray);
        //}
    }
}
