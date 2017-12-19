using System;
using System.Collections.Generic;
using System.Text;
using DataTransferObjects.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utils.AbstractClasses;

namespace DataAccess.Maps
{
    public class AuthorsMap : EntityTypeConfiguration<Author>
    {
        public override void Map(EntityTypeBuilder<Author> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);

            entity.Property(e => e.DateOfBirth).IsRequired();


        }
    }
}
