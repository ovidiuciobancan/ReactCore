using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Utils.CustumBinders;
using Utils.ExtensionMethods.OData;
using Utils.Extensions.Mapper;
using AutoMapper.QueryableExtensions;
using LinqToQuerystring;

namespace Utils.Helpers
{
    [ModelBinder(BinderType = typeof(ODataQueryBinder))]
    public class ODataQuery
    {
        const int MAX_PAGE_SIZE = 100;

        private ODataQueryParams ODataQueryParams;
        private MapperService Mapper;


        public ODataQuery(ODataQueryParams oDataQueryParams, MapperService mapper)
        {
            ODataQueryParams = oDataQueryParams;
            Mapper = mapper;
        }

        public IQueryable<T> ApplyTo<T>(IQueryable<T> source)
        {
            return source.LinqToQuerystring(ODataQueryParams.QueryString, maxPageSize: 100);
        }
        public IQueryable<U> ApplyTo<T, U>(IQueryable<T> source)
        {
            var projection = source.ProjectTo<U>(Mapper.AutoMapper.ConfigurationProvider);
            var qs = ODataQueryParams.QueryString;

            try
            {
                return projection.LinqToQuerystring(qs, maxPageSize: 10);
            }
            catch (Exception e)
            {
                return projection;
            }

        }
    }

    public class ODataQueryParams
    {
        public string QueryString { get; set; }
        public string Filter { get; set; }
        public string Top { get; set; }
        public string Skip { get; set; }
        public string OrderBy { get; set; }
    }
}
