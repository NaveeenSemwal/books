
using Books.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Books.Data.EntityFramework.Contexts
{
    /// <summary>
    /// How to customization Asp.net Identity
    /// 
    /// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0
    /// </summary>
    public class BookContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole,
                                  IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Book> Books { get; set; }
        public BookContext(DbContextOptions<BookContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AlterIdentityTables(modelBuilder);

        }

        private static void AlterIdentityTables(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Identity");


            modelBuilder.Entity<ApplicationUser>(b =>
            {
                //// Each User can have many UserClaims
                //b.HasMany(e => e.Claims)
                //    .WithOne()
                //    .HasForeignKey(uc => uc.UserId)
                //    .IsRequired();

                //// Each User can have many UserLogins
                //b.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });



            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User", schema: "Identity");
            });
            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role", schema: "Identity");
            });
            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable("UserRoles", schema: "Identity");
            });
            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims", schema: "Identity");
            });
            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins", schema: "Identity");
            });
            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims", schema: "Identity");
            });
            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens", schema: "Identity");
            });
        }
    }
}
