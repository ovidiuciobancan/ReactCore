using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Utils.Extensions.Mapper
{
    /// <summary>
    /// Base Mapper for AutoMapper configurations
    /// </summary>
    public abstract class BaseMapper
    {
        protected IMapper Mapper;

        public BaseMapper(IMapper mapper)
        {
            Mapper = mapper;
        }

        public abstract void Config(IMapperConfigurationExpression config);
    }

    /// <summary>
    /// Generic IMapper interface for T, U
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IMapper<T, U>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        U Map(T source);
    }

    public interface IPartialMapper<T, U>
    {
        void Map(T source, U destination);
    }

    /// <summary>
    /// Mapper service used to aggregate all mappers
    /// </summary>
    public class MapperService
    {
        private IServiceProvider Services;
        public IMapper AutoMapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MapperService(IServiceProvider services, IMapper mapper)
        {
            Services = services;
            AutoMapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public U Map<T, U>(T source)
        {
            var converter = Services.GetService<IMapper<T, U>>();

            return converter.Map(source);
        }

        public void Map<T, U>(T source, U destination)
        {
            var converter = Services.GetService<IPartialMapper<T, U>>();

            converter.Map(source, destination);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MapperServiceExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns>ISericeCollection for fluent API calls</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            Func<Type, bool> exprGoodInterfaces = (i) =>
                i.Name.Contains(typeof(IMapper<int, int>).Name) ||
                i.Name.Contains(typeof(IPartialMapper<int, int>).Name);

            var allAssemblies = new List<Assembly> { Assembly.GetCallingAssembly() }
                .Union(
                    Assembly.GetCallingAssembly()
                        .GetReferencedAssemblies()
                        .Select(assName => Assembly.Load(assName)))
                .ToList();


            var allInterfaces = allAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Any(exprGoodInterfaces))
                .ToList();

            allInterfaces.ForEach(timpl =>
            {
                var allMappers = timpl.GetTypeInfo().GetInterfaces()
                                    .Where(exprGoodInterfaces)
                                    .ToList();

                allMappers.ForEach(m =>
                {
                    services.AddScoped(m, timpl);
                });
            });


            return services.AddScoped<MapperService>();
        }

        /// <summary>
        /// Add AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mappers = typeof(BaseMapper).GetDerivedTypes(Assembly.GetEntryAssembly())
                .Where(t =>
                    !t.GetTypeInfo().IsInterface &&
                    !t.GetTypeInfo().IsAbstract)
                .ToList();

            var configMethodInterface = typeof(BaseMapper).GetMethods().FirstOrDefault()?.Name;


            var config = new MapperConfiguration(cfg =>
            {
                mappers.ForEach(mapperClass =>
                {
                    var ctorParams = mapperClass
                        .GetConstructors()
                        .FirstOrDefault()?
                        .GetParameters()?
                        .Select(p => default(object))
                        .ToArray() ?? new object[] { };

                    mapperClass.GetMethod(configMethodInterface)?.Invoke(Activator.CreateInstance(mapperClass, ctorParams as object[]), new[] { cfg });
                });
            });

            var mapper = config.CreateMapper();
            return services.AddSingleton(mapper);
        }

        public static IEnumerable<U> Map<T, U>(this MapperService mapperService, IEnumerable<T> collection)
        {
            return collection.Select(p => mapperService.Map<T, U>(p));
        }

        public static IQueryable<U> Map<T, U>(this MapperService mapperService, IQueryable<T> collection)
        {
            return collection.ProjectTo<U>(mapperService.AutoMapper.ConfigurationProvider);
        }

        //public static Expression<Func<U, bool>> Map<T, U>(this MapperService mapperService, Expression<Func<T, bool>> source)
        //{
        //    return mapperService.AutoMapper.Map<Expression<Func<U, bool>>>(source);
        //}

        //public static Expression<Func<U, R>> Map<T, U, R>(this MapperService mapperService, Expression<Func<T, R>> source)
        //{
        //    return mapperService.AutoMapper.Map<Expression<Func<U, R>>>(source);
        //}

        //public static IQueryable<U> Map<T, U>(this MapperService mapperService, IQueryable<T> source)
        //{
        //    return mapperService.AutoMapper.Map<IQueryable<U>>(source);
        //}

        //public static IQueryable<U> Map<T, U>(this MapperService mapperService, IQueryable<T> source, IQueryable<U> destination)
        //{
        //    return mapperService.AutoMapper.Map(source, destination);
        //}
    }
}

