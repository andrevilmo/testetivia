using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductRateConfiguration : IEntityTypeConfiguration<ProductRate>
{
    

    void IEntityTypeConfiguration<ProductRate>.Configure(EntityTypeBuilder<ProductRate> builder)
    {
         builder.ToTable("ProductRate");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Rate);
        builder.Property(u => u.Count);
        builder.Property(u => u.UpdatedAt);

    }
}
