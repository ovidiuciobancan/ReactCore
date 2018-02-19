using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.Models.Common
{
    public abstract class ResourceBaseDTO
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
