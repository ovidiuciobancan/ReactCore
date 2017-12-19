﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models.Authors
{
    public class AuthorVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
    }
}
