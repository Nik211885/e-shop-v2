using Core.Entities;
using Core.Entities.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class Configuration : IEntityTypeConfiguration<EntityBoundTest>
    {
        public void Configure(EntityTypeBuilder<EntityBoundTest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(10).IsRequired();
        }
    }
}