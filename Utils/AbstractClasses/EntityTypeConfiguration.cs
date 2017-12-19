using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Utils.AbstractClasses
{
    public abstract class EntityTypeConfiguration<TEntity>
              where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> entity);
    }

}
