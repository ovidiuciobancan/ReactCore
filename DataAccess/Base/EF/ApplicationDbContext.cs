using Microsoft.EntityFrameworkCore;

using DA.Maps;
using DataAccess.Entities;
using Utils.ExtensionMethods;
using DataTransferObjects.Entities;
using DataAccess.Maps;

namespace DA.Base
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            AddConfiguration(modelBuilder);

        }

        private void AddConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new AppCacheItemsMap());
            modelBuilder.AddConfiguration(new AuthorsMap());
            modelBuilder.AddConfiguration(new BooksMap());
        }

        #region DbSet
        public virtual DbSet<AppCacheItems> AppCacheItems { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        #endregion
    }
}
