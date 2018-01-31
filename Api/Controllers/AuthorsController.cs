using System;
using Microsoft.AspNetCore.Mvc;

using Api.Base;
using DataTransferObjects.Entities;
using Api.Models.Authors;
using Utils.Extensions.Mapper;
using Utils.Helpers;
using BusinessLogic.Base;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.Edm;
using Microsoft.Data.OData;
using System.Linq.Expressions;
using Microsoft.OData.UriParser;
using Utils.ExtensionMethods.OData;
using Api.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [Route("api/Authors")]
    [Produces("application/json")]
    public class AuthorsController :  BaseController//ResourceController<AuthorDTO>
    {
        private EntityService<Author> AuthorsService;

        public AuthorsController(EntityService<Author> authorsService, MapperService mapper)
            : base(mapper)
        {
            AuthorsService = authorsService;
        }
       
        [HttpGet(Name = nameof(GetAuthors))]
        public IActionResult GetAuthors()
        {
            var authors = AuthorsService.Get();

            var result = Mapper.Map<Author, AuthorDTO>(authors); 

            return Ok(result.ToList());
        }

        [HttpGet("{id}", Name = nameof(GetAuthor))]
        public IActionResult GetAuthor(Guid id)
        {
            var entity = AuthorsService.Get(id);
            if (entity == null) return NotFound();

            var result = Mapper.Map<Author, AuthorDTO>(entity);

            return Ok(result);
        }
        
        [HttpPost(Name = nameof(CreateAuthor))]
        public IActionResult CreateAuthor([FromBody]AuthorCreateDTO model)
        {
            if (model == null) return BadRequest();
            if (!ModelState.IsValid) return UnprocessableEntity();

            var entity = Mapper.Map<AuthorCreateDTO, Author>(model);

            AuthorsService.Add(entity);

            var createdModel = Mapper.Map<Author, AuthorDTO>(entity);

            return CreatedAtRoute("GetAuthor", new { id = entity.Id }, createdModel);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = nameof(DeleteAuthor))]
        public IActionResult DeleteAuthor(Guid id)
        {
            if (!AuthorsService.Exists(id)) return NotFound();

            AuthorsService.Remove(id);

            return NoContent();
        }

        [HttpPut("{id}", Name = nameof(UpdateAuthor))]
        public IActionResult UpdateAuthor(Guid id, [FromBody] AuthorUpdateDTO model)
        {
            if (model == null) return BadRequest();
            if (!ModelState.IsValid) return UnprocessableEntity();

            var entity = AuthorsService.Get(id);

            if (entity == null) return NotFound();

            Mapper.Map(model, entity);

            AuthorsService.Update(entity);

            return NoContent();
        }

        //public override AuthorDTO CreateLinksForResource(AuthorDTO resource)
        //{
        //    resource.Links = new List<LinkDTO>()
        //    {
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(GetAuthor), new { id = resource.Id }),
        //            Rel = "self",
        //            Method = HttpMethods.Get
        //        },
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(DeleteAuthor), new { id = resource.Id }),
        //            Rel = "delete_author",
        //            Method = HttpMethods.Delete
        //        },
        //        new LinkDTO
        //        {
        //            Href = Url.Link(nameof(UpdateAuthor), new { id = resource.Id }),
        //            Rel = "update_author",
        //            Method = HttpMethods.Put
        //        }
        //    };

        //    return resource;
        //}
        //public override LinkedCollectionDTO<AuthorDTO> CreateLinksForResourceCollection(LinkedCollectionDTO<AuthorDTO> linkedCollection)
        //{
        //    linkedCollection.Links = new List<LinkDTO>
        //    {
        //            new LinkDTO
        //        {
        //            Href = Url.Link(nameof(CreateAuthor), new { }),
        //            Rel = "create_author",
        //            Method = HttpMethods.Post
        //        }
        //    };

        //    return linkedCollection;
        //}
    }
}
