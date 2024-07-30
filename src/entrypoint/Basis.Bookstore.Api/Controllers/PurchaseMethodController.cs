using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Api.Presenters;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Create;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Find;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.FindById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basis.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseMethodController : ControllerBase
    {
        // GET: api/<PurchaseMethodController>
        private readonly IMediator _mediator;

        public PurchaseMethodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PurchaseMethodController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new ListPurchaseMethodCommand());

            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // GET api/<PurchaseMethodController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetByIdPurchaseMethodCommand(id));
            return DefaultPresenter.Cast(result, HttpStatusCode.OK);
        }

        // POST api/<PurchaseMethodController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseMethodModel purchaseMethod)
        {
            var result = await _mediator.Send(new CreatePurchaseMethodCommand()
            {
                BookId = purchaseMethod.BookId,
                Price = purchaseMethod.Price,                
                Id = purchaseMethod.Id,
            });

            return DefaultPresenter.Cast(result, HttpStatusCode.Created);
        }
    }
}
