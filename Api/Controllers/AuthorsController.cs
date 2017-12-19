using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Api.Base;
using Api.Extensions.Mapper;
using DataTransferObjects.Entities;
using BusinessLogic.Services;
using Api.Models.Authors;

namespace Api.Controllers
{
    [Route("api/Authors")]
    [Produces("application/json")]
    public class AuthorsController : BaseController
    {
        private AuthorsService AuthorsService;

        public AuthorsController(AuthorsService authorsService, MapperService mapper)
            : base(mapper)
        {
            AuthorsService = authorsService;
        }


        [HttpGet]
        //[Authorize(Roles="PayingUser")]
        public IActionResult Get()
        {
            var result = Mapper.Map<Author, AuthorVM>(AuthorsService.Get()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(Guid id)
        {
            var entity = AuthorsService.Get(id);
            if (entity == null) return NotFound();

            var result = Mapper.Map<Author, AuthorVM>(entity);

            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody]AuthorCreateVM model)
        {
            if (model == null) return BadRequest();

            var entity = Mapper.Map<AuthorCreateVM, Author>(model);

            AuthorsService.Add(entity);

            var createdModel = Mapper.Map<Author, AuthorVM>(entity);

            return CreatedAtRoute("GetAuthor", new { id = entity.Id }, createdModel);
        }


        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(Guid id)
        {
            if (!AuthorsService.Exists(id)) return NotFound();

            AuthorsService.Remove(id);

            return NoContent();
        }

        
    }
}
