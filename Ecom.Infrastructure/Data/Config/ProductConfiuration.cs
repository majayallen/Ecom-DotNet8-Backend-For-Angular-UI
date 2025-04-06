using Ecom.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiuration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=>x.Description).IsRequired();
            builder.Property(x=>x.NewPrice).HasColumnType("decimal(18,2)");
            builder.HasData(new Product
            {
                Id = 1,
                Name = "test",
                Description = "test",
                CategoryId = 1,
                NewPrice = 1000,
                OldPrice = 1100,
            });
        }
    }
}
