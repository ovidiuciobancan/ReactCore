//using System;
//using System.Collections.Generic;
//using System.Text;
//using Utils.Interfaces;

//namespace Utils.ExtensionMethods
//{
//    public static class MappingExtensionMethods
//    {
//        public static T ToEntity<T>(this IMapTo<T> source)
//        {
//            var mapper = ServiceProviderAccessor.ServiceProvider.GetService<AutoMapper.IMapper>();
//            return mapper.Map<T>(source);
//        }

//        public static T ToModel<U, T>(this U source)
//            where T : IMapFrom<U>
//        {
//            var mapper = ServiceProviderAccessor.ServiceProvider.GetService<AutoMapper.IMapper>();
//            return mapper.Map<T>(source);
//        }

//    }
//}
