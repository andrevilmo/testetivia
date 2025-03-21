using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    void IEntityTypeConfiguration<Cart>.Configure(EntityTypeBuilder<Cart> builder)
    {
         builder.ToTable("Cart");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Date);
        builder.Property(u => u.Discount);
        builder.Property(u => u.UserId); 
        builder.Property(u => u.UpdatedAt);
        builder.Property(u => u.CreatedAt);
        builder.HasMany(u => u.Products)
                .WithOne(e => (Cart) e.Cart)
                .HasForeignKey(g => g.CartId );
    }
}