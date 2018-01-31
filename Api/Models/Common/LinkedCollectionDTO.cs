using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Common
{
    public class LinkedCollectionDTO<T> : ResourceBaseDTO
        where T : ResourceBaseDTO
    {
        public LinkedCollectionDTO(IEnumerable<T> resourceCollection)
        {
            Value = resourceCollection ?? new List<T>();
        }

        public IEnumerable<T> Value { get; private set; }
    }
}
