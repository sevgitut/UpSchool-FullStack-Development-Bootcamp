using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //ID
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //ProductId
            //builder.HasKey(x => x.OrderId);
            ////builder.Property(x => x.OrderId).ValueGeneratedOnAdd();

            //Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);


            //Picture
            builder.Property(x => x.Picture)
                .IsRequired()
                .HasMaxLength(500);

            //IsOnSale
            builder.Property(x => x.IsOnSale).IsRequired();

            //Price
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18, 2)") // Fiyat alanının veritabanındaki sütun tipini belirtme
                .IsRequired();

            //SalePrice
            builder.Property(x => x.SalePrice)
                .HasColumnType("decimal(18, 2)");

            //CreatedOn
            builder.Property(x => x.CreatedOn)
                .IsRequired()
                .ValueGeneratedOnAdd();

            //ModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            //DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            //IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
            builder.HasIndex(x => x.IsDeleted);



            builder.ToTable("Products");
        }
    }
}
