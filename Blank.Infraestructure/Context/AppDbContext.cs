using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blank.Infraestructure.Data.Context
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            var properties = modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties());

            foreach (var property in properties)
            {

                if (property.ClrType == typeof(decimal))
                    property.SetColumnType("decimal(8,3)");

            }

            modelBuilder
               .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


            SeedUsers(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUserRoles(modelBuilder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            string email = "admin@gmail.com";

            IdentityUser user = new()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = email,
                Email = email.ToUpper(),
                NormalizedEmail = email,
                NormalizedUserName = email,
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                ConcurrencyStamp = "8f9f7def-0822-4a1d-9b7e-0679caf9ac1d",
                SecurityStamp = "f85aacd2-07c5-4826-90b1-57281b5db5cf"
            };

            PasswordHasher<IdentityUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");

            builder.Entity<IdentityUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }
    }
}
