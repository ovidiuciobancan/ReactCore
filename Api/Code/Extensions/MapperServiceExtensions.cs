using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Api.Code.Base;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Utils.ExtensionMethods;

namespace Api.Extensions.Mapper
{
    /// <summary>
    /// Base IMapper for AutoMapper configurations
    /// </summary>
    public interface IBaseMapper
    {
        //void Config(IMapperConfigurationExpression config);
    }

    /// <summary>
    /// Generic IMapper interface for T, U
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface IMapper<T, U> : IBaseMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        U Map(T source);
    }

    public interface IPartialMapper<T, U> : IBaseMapper
    {
        void Map(T source, U destination);
    }

    /// <summary>
    /// Mapper service used to aggregate all mappers
    /// </summary>
    public class MapperService
    {
        private IServiceProvider Services;

        /// <summary>
        /// 
        /// </summary>
        public MapperService(IServiceProvider services)
        {
            Services = services;
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
    public static class MapperServiceExtensions
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

            var allAssemblies = new List<Assembly>
            {
                typeof(MapperService).GetTypeInfo().Assembly,
            };

            allAssemblies.AddRange(typeof(MapperService).GetTypeInfo().Assembly.GetReferencedAssemblies().ToList()
                .Select(assName =>
                {
                    return Assembly.Load(assName);
                }));

            allAssemblies.ForEach(assembly =>
            {
                var allInterfaces = typeof(Startup).GetTypeInfo()
                    .Assembly.GetTypes()
                    .Where(
                        t => t.GetInterfaces()
                            .Any(exprGoodInterfaces))
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
            var mappers = typeof(IBaseMapper).GetDerivedTypes(Assembly.GetEntryAssembly())
                .Where(t =>
                    !t.GetTypeInfo().IsInterface &&
                    !t.GetTypeInfo().IsAbstract)
                .ToList();

            var configMethodInterface = typeof(BaseMapper).GetMethods().FirstOrDefault()?.Name;
            

            var config = new MapperConfiguration(cfg =>
            {
                mappers.ForEach(mapperClass => {
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
    }
}

