using System;
using AutoMapper;
using Api.Models.Authors;
using DataTransferObjects.Entities;
using System.Linq;
using Api.Models.Books;
using Utils.Extensions.Mapper;
using System.Linq.Expressions;

namespace Api.Mappers
{
    public class AuthorMapper : BaseMapper,
        IMapper<Author, AuthorDTO>, 
        IMapper<AuthorDTO, Author>, 
        IMapper<AuthorCreateDTO, Author>,
        IMapper<Expression<Func<AuthorDTO, bool>>, Expression<Func<Author, bool>>>
    {
        private MapperService MapperService; 

        public AuthorMapper(IMapper mapper, MapperService mapperService)
            : base(mapper)
        {
            MapperService = mapperService;
        }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Author, AuthorDTO>()
                .ForMember(d => d.Name, a => a.MapFrom(s => $"{s.FirstName} {s.LastName}"))
                .ForMember(d => d.Age, a => a.MapFrom(s =>  DateTime.Now.Year - s.DateOfBirth.Year));
            config.CreateMap<AuthorDTO, Author>();
            config.CreateMap<AuthorCreateDTO, Author>();
        }

        public AuthorDTO Map(Author source)
        {
            return Mapper.Map<AuthorDTO>(source);
        }
        public Author Map(AuthorDTO source)
        {
            return Mapper.Map<Author>(source);
        }
        public Author Map(AuthorCreateDTO source)
        {
            var result = Mapper.Map<AuthorCreateDTO, Author>(source);
            result.Books = source.Books
                .Select(p => MapperService.Map<BookCreateDTO, Book>(p))
                .ToList();
            return result;
        }

        public Expression<Func<Author, bool>> Map(Expression<Func<AuthorDTO, bool>> source)
        {
            return Mapper.Map<Expression<Func<Author, bool>>>(source);
        }
    }
}
