using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Base;
using Api.Extensions.Mapper;
using Api.Models.Authors;
using BusinessLogic.Services;
using DataTransferObjects.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/AuthorCollection")]
    public class AuthorCollectionController : BaseController
    {
        private AuthorsService AuthorsService;

        public AuthorCollectionController(MapperService mapper, AuthorsService authorsService) 
            : base(mapper)
        {
            AuthorsService = authorsService;
        }

        [HttpGet("({ids})", Name = nameof(GetAuthorsCollection))]
        public IActionResult GetAuthorsCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null) return BadRequest();

            var entities = AuthorsService.Get(ids);

            var model = Mapper.Map<Author, AuthorVM>(entities);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody]IEnumerable<AuthorCreateVM> authors)
        {
            if (authors == null) return BadRequest();

            var entities = Mapper.Map<AuthorCreateVM, Author>(authors).ToList();

            AuthorsService.AddRange(entities);

            var model = Mapper.Map<Author, AuthorVM>(entities);
            var ids = entities
                .Select(p => p.Id.ToString())
                .Aggregate((i,j) => $"{i},{j}");

            return CreatedAtRoute(nameof(GetAuthorsCollection), new {ids }, model);
        }

        
    }
}