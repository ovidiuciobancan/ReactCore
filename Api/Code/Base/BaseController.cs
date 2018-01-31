using API.Helpers;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Utils.Enums;
using Utils.Extensions;
using Utils.Extensions.Mapper;
using Utils.Helpers;

namespace Api.Base
{
    public abstract class BaseController : Controller
    {
        protected MapperService Mapper;

        protected virtual ODataValidationSettings ODataValidationSettings => new ODataValidationSettings();

        public BaseController(MapperService mapper = null)
        {
            Mapper = mapper;
        }

        protected UnprocessableEntityObjectResult UnprocessableEntity()
        {
            return new UnprocessableEntityObjectResult(ModelState.GetErrors());
        }

        protected string CreateResourceUri(int pageNumber, int pageSize, PaginationLinkType type, string linkName)
        {
            return Url.Link(linkName, new
            {
                pageNumber = pageNumber + (int)type, //preview, next, current
                pageSize  
            });
        }

        protected void AddPaginationMetadata<T>(PagedList<T> pagedList, string linkName)
        {
            var previousPageLink = pagedList.HasPrevious ?
                CreateResourceUri(pagedList.CurrentPage, pagedList.PageSize, PaginationLinkType.PreviousPage, linkName) : null;
            var nextPageLink = pagedList.HasNext ?
                CreateResourceUri(pagedList.CurrentPage, pagedList.PageSize, PaginationLinkType.NextPage, linkName) : null;

            var paginationMetadata = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination", paginationMetadata.ToJson());
        }
    }
}
