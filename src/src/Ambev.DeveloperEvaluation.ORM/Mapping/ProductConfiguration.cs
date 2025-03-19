using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    

    void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
    {
         builder.ToTable("Product");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Category);
        builder.Property(u => u.CreatedAt);
        builder.Property(u => u.Description);
        builder.Property(u => u.Image);
        builder.Property(u => u.Price);
        builder.Property(u => u.Title);
        builder.HasOne(u => (ProductRate)u.Rating)
                .WithOne(e => (Product)e.Product)
                .HasForeignKey<ProductRate>(g => g.ProductId );
        builder.Property(u => u.UpdatedAt);

    }
}
