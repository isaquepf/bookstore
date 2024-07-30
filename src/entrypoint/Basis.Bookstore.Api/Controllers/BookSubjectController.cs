using Basis.Bookstore.Api.Presenters;
using Basis.Bookstore.Core.Application.UseCases.BookSubjects.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basis.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookSubjectController(IMediator mediator) => _mediator = mediator;

        // POST api/<BookSubjectController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookSubjectCommand subjectCommand)
        {         
            var result = await _mediator.Send(subjectCommand);

            return DefaultPresenter.Cast(result, HttpStatusCode.Created);
        }

    }
}
