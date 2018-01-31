using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Books;

namespace Api.Models.Authors
{
    public class AuthorUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Genre { get; set; }
    }
}
