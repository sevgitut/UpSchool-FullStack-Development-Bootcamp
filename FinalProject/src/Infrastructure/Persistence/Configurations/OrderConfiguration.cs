using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // ID
            builder.HasKey(x => x.Id);

            //RequestedAmount
            builder.Property(x => x.RequestedAmount).IsRequired();
           
            //TotalFoundAmount
            builder.Property(x => x.TotalFoundAmount).IsRequired();

            //Product Crawl Type
            builder.Property(x => x.ProductCrawlType)
                .IsRequired()
                .HasConversion<int>();

            //CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();
                
            //ModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            //DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            //IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
            builder.HasIndex(x => x.IsDeleted);

            //Relationships
            builder.HasMany<Product>(x => x.Products)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); ;

            builder.HasMany<OrderEvent>(x => x.OrderEvents)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); ;

            builder.ToTable("Orders");

           
        }
    }
}
