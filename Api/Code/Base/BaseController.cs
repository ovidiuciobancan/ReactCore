using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Extensions.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Base
{
    public abstract class BaseController : Controller
    {
        protected MapperService Mapper;

        public BaseController(MapperService mapper)
        {
            Mapper = mapper;
        }
    }
}
