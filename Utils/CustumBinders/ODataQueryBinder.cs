using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Utils.Extensions;
using Utils.Extensions.Mapper;
using Utils.Helpers;

namespace Utils.CustumBinders
{
    public class ODataQueryBinder : IModelBinder
    {
        private MapperService Mapper;

        public ODataQueryBinder(MapperService mapper)
        {
            Mapper = mapper;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var queryOptions = new ODataQueryParams
            {
                QueryString = System.Net.WebUtility.UrlDecode(bindingContext.HttpContext.Request.QueryString.ToNullString()),
                Filter = bindingContext.HttpContext.Request.Query["$filter"].ToNullString(),
                OrderBy = bindingContext.HttpContext.Request.Query["$orderby"].ToNullString(),
                Skip = bindingContext.HttpContext.Request.Query["$skip"].ToNullString(),
                Top = bindingContext.HttpContext.Request.Query["$top"].ToNullString(),
            };

            var model = new ODataQuery(queryOptions, Mapper);

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
