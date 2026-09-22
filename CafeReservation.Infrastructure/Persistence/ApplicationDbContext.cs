using CafeReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CafeReservation.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).IsRequired();

                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

                entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);


            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(sc => sc.Id);

                entity.HasMany(sc => sc.Items)
                .WithOne(sci => sci.Cart)
                .HasForeignKey(sci => sci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasKey(sci => sci.Id);

                entity.Property(sci => sci.Id)
                .UseIdentityColumn(seed: 1000, increment: 1);

                entity.Property(sci => sci.Price)
                .HasPrecision(18, 2);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
