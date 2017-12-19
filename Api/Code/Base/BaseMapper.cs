using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Api.Code.Base
{
    public abstract class BaseMapper
    {
        protected IMapper Mapper;

        public BaseMapper(IMapper mapper)
        {
            Mapper = mapper; 
        }

        public abstract void Config(IMapperConfigurationExpression config);
    }
}
