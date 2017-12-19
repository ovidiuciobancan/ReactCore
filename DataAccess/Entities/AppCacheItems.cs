using System;
using System.Collections.Generic;
using Common.Interfaces;

namespace DataAccess.Entities
{
    public partial class AppCacheItems : ICacheItem
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
