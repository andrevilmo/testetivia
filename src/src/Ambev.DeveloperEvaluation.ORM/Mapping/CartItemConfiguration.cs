using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    

    void IEntityTypeConfiguration<CartItem>.Configure(EntityTypeBuilder<CartItem> builder)
    {
         builder.ToTable("CartItem");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.ProductId);
        builder.Property(u => u.Quantity);
        builder.Property(u => u.UpdatedAt);
        builder.Property(u => u.CreatedAt);
        builder.HasOne(u => u.Cart) 
        .WithMany(e => (IEnumerable<CartItem>) e.Products)
        .HasForeignKey(e => e.CartId);

    }
}
