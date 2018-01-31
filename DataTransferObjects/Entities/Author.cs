using System;
using System.Collections.Generic;
using System.Text;
using Common.Interfaces;

namespace DataTransferObjects.Entities
{
    public class Author : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset? DateofDeath { get; set; }

        public string Genre { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
