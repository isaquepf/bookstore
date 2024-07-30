using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Api.Presenters;
using Basis.Bookstore.Core.Application.UseCases.Author.Create;
using Basis.Bookstore.Core.Application.UseCases.Author.Delete;
using Basis.Bookstore.Core.Application.UseCases.Author.Find;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Basis.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new ListAuthorCommand());

            return  DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult>  Get(int id)
        {
            var result = await _mediator.Send(new FindByIdAuthorCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorModel author)
        {           
            var result = await _mediator.Send(new CreateAuthorCommand
            {
                Name = author.Name
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.Created);
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorModel author)
        {
            var result = await _mediator.Send(new UpdateAuthorCommand
            {
                Id = id,                
                Author = new CreateAuthorCommand()
                {
                    Name = author.Name
                },                
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }
    }
}
