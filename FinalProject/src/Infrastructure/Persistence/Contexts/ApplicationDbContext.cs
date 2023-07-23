using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Configurations
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderEventConfiguration());

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //    .HasOne(x => x.Order)
            //    .WithMany(x => x.Products)
            //    .HasForeignKey(x => x.OrderId);

            //modelBuilder.Entity<OrderEvent>()
            //    .HasOne(x => x.Order)
            //    .WithMany(x => x.OrderEvents)
            //    .HasForeignKey(x => x.OrderId);
        }
    }
}
