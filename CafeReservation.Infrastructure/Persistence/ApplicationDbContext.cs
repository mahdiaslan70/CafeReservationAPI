using System;
using System.Collections.Generic;
using System.Text;
using CafeReservation.Domain;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
