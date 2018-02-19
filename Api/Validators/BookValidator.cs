using BusinessLogic.Services;
using DTO.Models.Books;
using FluentValidation;

namespace Api.Validators
{
    public class BookCreateValidator : AbstractValidator<BookCreateDTO>
    {
        private BooksService BooksService;

        public BookCreateValidator(BooksService booksService)
        {
            BooksService = booksService;

            RuleFor(p => p.Title).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Description);

            RuleFor(p => p).Must(BeUnique).WithName("_error");
        }

        private bool BeUnique(BookCreateDTO model)
        {
            var  x = BooksService.Get();
            return true;
        }
    }

    public class BookUpdateValidator : AbstractValidator<BookUpdateDTO>
    {
        private BooksService BooksService;

        public BookUpdateValidator(BooksService booksService)
        {
            BooksService = booksService;

            RuleFor(p => p.Title).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Description);

            RuleFor(p => p).Must(BeUnique).WithName("_error");
        }

        private bool BeUnique(BookUpdateDTO model)
        {
            return true;
        }
    }


}
