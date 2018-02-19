using System;
using DTO.Models.Common;

namespace DTO.Models.Authors
{
    public class AuthorDTO : ResourceBaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
    }
}
