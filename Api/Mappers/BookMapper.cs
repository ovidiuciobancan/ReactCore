using AutoMapper;

using Api.Models.Books;
using DataTransferObjects.Entities;
using Utils.Extensions.Mapper;

namespace Api.Mappers
{
    public class BookMapper : BaseMapper,
        IMapper<Book, BookDTO>, 
        IMapper<BookCreateDTO, Book>
        
    {
        public BookMapper(IMapper mapper) : base(mapper) { }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Book, BookDTO>();
            config.CreateMap<BookCreateDTO, Book>();

        }

        public BookDTO Map(Book source)
        {
            return Mapper.Map<Book, BookDTO>(source);
        }

        public Book Map(BookCreateDTO source)
        {
            return Mapper.Map<BookCreateDTO, Book>(source);
        }
    }

    public class BookUpdateMapper : BaseMapper,
        IMapper<Book, BookUpdateDTO>,
        IPartialMapper<BookUpdateDTO, Book>
    {
        public BookUpdateMapper(IMapper mapper) : base(mapper) { }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Book, BookUpdateDTO>();
            config.CreateMap<BookUpdateDTO, Book>();
        }
        public void Map(BookUpdateDTO source, Book destination)
        {
            Mapper.Map(source, destination);
        }

        public BookUpdateDTO Map(Book source)
        {
            return Mapper.Map<BookUpdateDTO>(source);
        }

    }
}
