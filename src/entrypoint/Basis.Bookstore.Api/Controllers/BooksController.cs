using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Api.Presenters;
using Basis.Bookstore.Core.Application.UseCases.Book.Create;
using Basis.Bookstore.Core.Application.UseCases.Books.Delete;
using Basis.Bookstore.Core.Application.UseCases.Books.Find;
using Basis.Bookstore.Core.Application.UseCases.Books.FindById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBook.Application.UseCases.Book.Update;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Basis.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new FindBookCommand());

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new FindByIdBookCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookModel model)
        {
            var result = await _mediator.Send(new CreateBookCommand
            {
                AuthorsIds = model.AuthorsIds,
                Description = model.Description,
                Edition = model.Edition,
                PublishedAt = model.PublishedYear,
                Publisher = model.Publisher,
                Title = model.Title,
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.Created);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookModel model)
        {
            var result = await _mediator.Send(new UpdateBookCommand
            {
                Id = id,
                Book = new CreateBookCommand
                {
                    Id = id,
                    AuthorsIds = model.AuthorsIds,
                    Description = model.Description,
                    Edition = model.Edition,
                    PublishedAt = model.PublishedYear,
                    Publisher = model.Publisher,
                    Title = model.Title,
                }
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }
    }
}
