using System;

namespace DTO.Models.Books
{
    public class BookCreateDTO
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
