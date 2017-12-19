using System;
using Microsoft.AspNetCore.Mvc;

using Api.Base;
using Api.Extensions.Mapper;
using BusinessLogic.Services;
using DataTransferObjects.Entities;
using Api.Models.Books;
using Microsoft.AspNetCore.JsonPatch;

namespace Api.Controllers
{
    //[Route("api/Books")]
    [Produces("application/json")]
    public class BooksController : BaseController
    {
        private BooksService BooksService;
        private AuthorsService AuthorsService;

        public BooksController(MapperService mapper, BooksService booksService, AuthorsService authorsService)
            : base(mapper)
        {
            BooksService = booksService;
            AuthorsService = authorsService;
        }

        [HttpGet]
        [Route("api/books", Name = nameof(GetBooks))]
        public IActionResult GetBooks()
        {
            var entities = BooksService.Get();
            var result = Mapper.Map<Book, BookVM>(entities);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/authors/{authorId}/books", Name = nameof(GetBooksByAuthor))]
        public IActionResult GetBooksByAuthor(Guid authorId)
        {
            if (!AuthorsService.Exists(authorId)) return NotFound();

            var entities = BooksService.GetByParent(authorId);
            var result = Mapper.Map<Book, BookVM>(entities);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/books/{id}", Name = nameof(GetBook))]
        public IActionResult GetBook(Guid id)
        {
            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            var result = Mapper.Map<Book, BookVM>(entity);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/authors/{authorId}/books", Name = nameof(CreateBook))]
        public IActionResult CreateBook(Guid authorId, [FromBody] BookCreateVM model)
        {
            if (model == null) return BadRequest();
            if (!AuthorsService.Exists(authorId)) return NotFound();

            model.AuthorId = authorId;
            var entity = Mapper.Map<BookCreateVM, Book>(model);

            BooksService.Add(entity);

            var createdModel = Mapper.Map<Book, BookVM>(entity);

            return CreatedAtRoute(nameof(GetBook), new { id = entity.Id }, createdModel);
        }

        [HttpDelete]
        [Route("api/books/{id}", Name = nameof(DeleteBook))]
        public IActionResult DeleteBook(Guid id)
        {
            if (!BooksService.Exists(id)) return NotFound();

            BooksService.Remove(id);

            return NoContent();
        }

        [HttpPut]
        [Route("api/books/{id}", Name = nameof(UpdateBook))]
        public IActionResult UpdateBook(Guid id, [FromBody] BookUpdateVM model)
        {
            if (model == null) return BadRequest();

            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            Mapper.Map(model, entity);

            BooksService.Update(entity);

            return NoContent();
        }

        [HttpPatch]
        [Route("api/books/{id}", Name = nameof(PartialUpdateBook))]
        public IActionResult PartialUpdateBook(Guid id, [FromBody] JsonPatchDocument<BookUpdateVM> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            var model = Mapper.Map<Book, BookUpdateVM>(entity);

            patchDocument.ApplyTo(model);

            //add validation

            Mapper.Map(model, entity);

            BooksService.Update(entity);

            return NoContent();
        }
    }
}