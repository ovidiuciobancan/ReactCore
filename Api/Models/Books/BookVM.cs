using System;

namespace Api.Models.Books
{
    public class BookVM
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }
    }
}