using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed the roles (Admin and Users)


            var adminRoleID = "567e8edd-ad8e-4c2c-804c-514b0477dda3";
            var userRoleID = "b31c9846-6b6c-4dad-bbf5-51b774343124";



            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleID,
                    ConcurrencyStamp = adminRoleID
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleID,
                    ConcurrencyStamp = userRoleID
                }


            };

            //seeding roles in db
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Admin
            var AdminID = "280549d0-a41d-42e2-895e-599f191b2f71";
            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Id = AdminID
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin@123");

            builder.Entity<IdentityUser>().HasData(admin);


            //add roles to admin

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleID,
                    UserId = AdminID,
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleID,
                    UserId = AdminID,
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);


            base.OnModelCreating(builder);

            // Configure one-to-one relationship between Profile and IdentityUser
            builder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId) // Use the same type (string) for the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Adjust the delete behavior as needed

        }
        


    }
}
