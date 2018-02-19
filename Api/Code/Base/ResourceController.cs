using System.Collections.Generic;
using DTO.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Utils.Extensions.Mapper;

namespace Api.Base
{
    public abstract class ResourceController<T> : BaseController
        where T : ResourceBaseDTO
    {
        public ResourceController(MapperService mapper)
            : base(mapper) { }

        public abstract T CreateLinksForResource(T resource);
        public abstract LinkedCollectionDTO<T> CreateLinksForResourceCollection(LinkedCollectionDTO<T> linkedCollection);

        public virtual OkObjectResult Ok(T resource)
        {
            return base.Ok(CreateLinksForResource(resource));
        }
        
        public virtual OkObjectResult Ok(List<T> resourceCollection)
        {
            resourceCollection.ForEach(p => CreateLinksForResource(p));
            var result = CreateLinksForResourceCollection(new LinkedCollectionDTO<T>(resourceCollection));
            return base.Ok(result);
        }

        public virtual CreatedAtRouteResult CreatedAtRoute(string routeName, object routeValues, T resource)
        {
            return base.CreatedAtRoute(routeName, routeValues, CreateLinksForResource(resource));
        }
    }
}