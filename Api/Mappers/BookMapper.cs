using AutoMapper;

using Api.Extensions.Mapper;
using Api.Models.Books;
using DataTransferObjects.Entities;
using Api.Code.Base;

namespace Api.Mappers
{
    public class BookMapper : BaseMapper,
        IMapper<Book, BookVM>, 
        IMapper<BookCreateVM, Book>
        
    {
        public BookMapper(IMapper mapper) : base(mapper) { }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Book, BookVM>();
            config.CreateMap<BookCreateVM, Book>();

        }

        public BookVM Map(Book source)
        {
            return Mapper.Map<Book, BookVM>(source);
        }

        public Book Map(BookCreateVM source)
        {
            return Mapper.Map<BookCreateVM, Book>(source);
        }
    }

    public class BookUpdateMapper : BaseMapper,
        IMapper<Book, BookUpdateVM>,
        IPartialMapper<BookUpdateVM, Book>
    {
        public BookUpdateMapper(IMapper mapper) : base(mapper) { }

        public override void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Book, BookUpdateVM>();
            config.CreateMap<BookUpdateVM, Book>();
        }
        public void Map(BookUpdateVM source, Book destination)
        {
            Mapper.Map(source, destination);
        }

        public BookUpdateVM Map(Book source)
        {
            return Mapper.Map<BookUpdateVM>(source);
        }

    }
}
