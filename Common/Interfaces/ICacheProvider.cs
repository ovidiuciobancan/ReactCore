using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interfaces
{
    public interface ICacheProvider
    {
        /// <summary>
        /// This method get item from cache if exists or calls predicate to retrieve data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get<T>(CacheItems type, Func<T> predicate) where T : class;
        void Invalidate(CacheItems type);
    }
}
