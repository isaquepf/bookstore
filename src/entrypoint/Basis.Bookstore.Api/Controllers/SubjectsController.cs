using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Api.Presenters;
using Basis.Bookstore.Core.Application.UseCases.Author.Create;
using Basis.Bookstore.Core.Application.UseCases.Author.Delete;
using Basis.Bookstore.Core.Application.UseCases.Author.Find;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors.Update;
using Basis.Bookstore.Core.Application.UseCases.Subject.Create;
using Basis.Bookstore.Core.Application.UseCases.Subject.Find;
using Basis.Bookstore.Core.Application.UseCases.Subject.Update;
using Basis.Bookstore.Core.Application.UseCases.Subjects.FindById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBook.Application.UseCases.Subject.Delete;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Basis.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator) => _mediator = mediator;

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new ListSubjectCommand());

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetByIdSubjectCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubjectModel model)
        {
            var result = await _mediator.Send(new CreateSubjectCommand
            {
                Description = model.Description,
                BookId = model.Id,
                Id = model.Id
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.Created);
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubjectModel model)
        {
            var result = await _mediator.Send(new UpdateSubjectCommand
            {
                Id = id,
                Subject = new CreateSubjectCommand
                {
                    Description = model.Description
                }
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSubjectCommand(id));

            return DefaultPresenter.Cast(result, HttpStatusCode.NoContent);
        }
    }
}
