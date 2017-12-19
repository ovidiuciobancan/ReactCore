using System;
using System.Collections.Generic;
using System.Text;
using DataTransferObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utils.AbstractClasses;

namespace DataAccess.Maps
{
    public class BooksMap : EntityTypeConfiguration<Book>
    {
        public override void Map(EntityTypeBuilder<Book> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            entity.HasOne(d => d.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
