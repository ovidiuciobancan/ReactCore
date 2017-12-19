using Utils.AbstractClasses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DA.Maps
{
    public class AppCacheItemsMap : EntityTypeConfiguration<AppCacheItems>
    {
        public override void Map(EntityTypeBuilder<AppCacheItems> entity)
        {
            entity.ToTable("AppCacheItems", "utl");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.LastModifiedDate)
                .IsRequired()
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
