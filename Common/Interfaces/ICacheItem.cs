using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ICacheItem
    {
        short Id { get; set; }
        string Name { get; set; }
        DateTime LastModifiedDate { get; set; }
    }
}
