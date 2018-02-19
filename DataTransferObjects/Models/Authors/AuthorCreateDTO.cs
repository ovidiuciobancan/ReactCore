using System;
using System.Collections.Generic;
using DTO.Models.Books;

namespace DTO.Models.Authors
{
    public class AuthorCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        public DateTimeOffset? DateofDeath { get; set; }

        public string Genre { get; set; }

        public ICollection<BookCreateDTO> Books { get; set; } = new List<BookCreateDTO>();
    }
}
