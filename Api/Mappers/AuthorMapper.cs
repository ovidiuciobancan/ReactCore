using System;
using AutoMapper;

using Api.Extensions.Mapper;
using Api.Models.Authors;
using DataTransferObjects.Entities;
using System.Linq;
using Api.Models.Books;
using Api.Code.Base;

namespace Api.Mappers
{
    public class AuthorMapper : BaseMapper,
        IMapper<Author, AuthorVM>, 
        IMapper<AuthorVM, Author>, 
        IMapper<AuthorCreateVM, Author>
    {
        private MapperService MapperService; 

        public AuthorMapper(IMapper mapper, MapperService mapperService)
            : base(mapper)
        {
            MapperService = mapperService;
        }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Author, AuthorVM>()
                .ForMember(d => d.Name, a => a.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Age, a => a.MapFrom(s =>  DateTime.Now.Year - s.DateOfBirth.Year));
            config.CreateMap<AuthorVM, Author>();
            config.CreateMap<AuthorCreateVM, Author>();
        }

        public AuthorVM Map(Author source)
        {
            return Mapper.Map<AuthorVM>(source);
        }
        public Author Map(AuthorVM source)
        {
            return Mapper.Map<Author>(source);
        }
        public Author Map(AuthorCreateVM source)
        {
            var result = Mapper.Map<AuthorCreateVM, Author>(source);
            result.Books = source.Books
                .Select(p => MapperService.Map<BookCreateVM, Book>(p))
                .ToList();
            return result;
        }
    }
}
