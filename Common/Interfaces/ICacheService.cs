using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ICacheService
    {
        IEnumerable<ICacheItem> Get();
    }
}
