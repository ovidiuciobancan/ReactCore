using Utils.AbstractClasses;
using Microsoft.EntityFrameworkCore;


namespace Utils.ExtensionMethods
{
    /// <summary>
    /// EF Model Builder Extension Methods
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Add mapping configuration
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="configuration"></param>
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}