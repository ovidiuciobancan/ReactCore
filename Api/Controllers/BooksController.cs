using System;
using Microsoft.AspNetCore.Mvc;

using Api.Base;
using BusinessLogic.Services;
using DataTransferObjects.Entities;
using Api.Models.Books;
using Microsoft.AspNetCore.JsonPatch;
using Utils.Extensions.Mapper;
using System.Collections.Generic;
using Api.Models.Common;
using Microsoft.AspNetCore.Http;
using Utils.Helpers;

namespace Api.Controllers
{
    //[Route("api/Books")]
    [Produces("application/json")]
    public class BooksController : BaseController //ResourceController<BookDTO>
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

            var result = Mapper.Map<Book, BookDTO>(entities);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/authors/{authorId}/books", Name = nameof(GetBooksByAuthor))]
        public IActionResult GetBooksByAuthor(Guid authorId)
        {
            if (!AuthorsService.Exists(authorId)) return NotFound();

            var entities = BooksService.GetByParent(authorId);
            var result = Mapper.Map<Book, BookDTO>(entities);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/books/{id}", Name = nameof(GetBook))]
        public IActionResult GetBook(Guid id)
        {
            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            var result = Mapper.Map<Book, BookDTO>(entity);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/authors/{authorId}/books", Name = nameof(CreateBook))]
        public IActionResult CreateBook(Guid authorId, [FromBody] BookCreateDTO model)
        {
            if (model == null) return BadRequest();
            if (!AuthorsService.Exists(authorId)) return NotFound();
            if (!ModelState.IsValid) return UnprocessableEntity();

            model.AuthorId = authorId;
            var entity = Mapper.Map<BookCreateDTO, Book>(model);

            BooksService.Add(entity);

            var createdModel = Mapper.Map<Book, BookDTO>(entity);

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
        public IActionResult UpdateBook(Guid id, [FromBody] BookUpdateDTO model)
        {
            if (model == null) return BadRequest();
            if (!ModelState.IsValid) return UnprocessableEntity();

            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            Mapper.Map(model, entity);

            BooksService.Update(entity);

            return NoContent();
        }

        [HttpPatch]
        [Route("api/books/{id}", Name = nameof(PartialUpdateBook))]
        public IActionResult PartialUpdateBook(Guid id, [FromBody] JsonPatchDocument<BookUpdateDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var entity = BooksService.Get(id);

            if (entity == null) return NotFound();

            var model = Mapper.Map<Book, BookUpdateDTO>(entity);

            patchDocument.ApplyTo(model);

            if (!ModelState.IsValid) return UnprocessableEntity();

            Mapper.Map(model, entity);

            BooksService.Update(entity);

            return NoContent();
        }

        //public override BookDTO CreateLinksForResource(BookDTO resource)
        //{
        //    resource.Links = new List<LinkDTO>()
        //    {
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(GetBook), new { id = resource.Id }),
        //            Rel = "self",
        //            Method = HttpMethods.Get
        //        },
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(DeleteBook), new { id = resource.Id }),
        //            Rel = "delete_book",
        //            Method = HttpMethods.Delete
        //        },
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(UpdateBook), new { id = resource.Id }),
        //            Rel = "update_book",
        //            Method = HttpMethods.Put
        //        },
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(PartialUpdateBook), new { id = resource.Id }),
        //            Rel = "partial_update_book",
        //            Method = HttpMethods.Patch
        //        }
        //    };

        //    return resource;
        //}
    }
}