using System;
using Api.Models.Common;

namespace Api.Models.Books
{
    public class BookDTO : ResourceBaseDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid AuthorId { get; set; }
    }
}