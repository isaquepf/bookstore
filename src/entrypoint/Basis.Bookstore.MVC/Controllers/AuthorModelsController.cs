using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Core.Application.UseCases.Author.Create;
using Basis.Bookstore.Core.Application.UseCases.Author.Delete;
using Basis.Bookstore.Core.Application.UseCases.Author.Find;
using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors.Update;
using Basis.Bookstore.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basis.Bookstore.Mvc.Controllers
{
    public class AuthorModelsController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorModelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: AuthorModels
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListAuthorCommand());
            return View(result.Data);
        }

        ///GET: AuthorModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _mediator.Send(new FindByIdAuthorCommand(id.Value));

            if (authorModel.Data == null)
            {
                return NotFound();
            }

            return View(authorModel.Data);
        }

        //GET: AuthorModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AuthorModel authorModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateAuthorCommand
                {
                    Name = authorModel.Name
                });

                return RedirectToAction(nameof(Index));
            }

            return View(authorModel);
        }

        // GET: AuthorModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var result = await _mediator.Send(new FindByIdAuthorCommand(id.Value));

            var authorModel = result.Data;

            if (authorModel == null)
            {
                return NotFound();
            }

            return View(authorModel);
        }

        // POST: AuthorModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AuthorModel authorModel)
        {
            if (id != authorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateAuthorCommand
                {
                    Id = id,
                    Author = new CreateAuthorCommand()
                    {
                        Name = authorModel.Name
                    },
                });

                return RedirectToAction(nameof(Index));
            }
            return View(authorModel);
        }

        // GET: AuthorModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new FindByIdAuthorCommand(id.Value));

            
            if (result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: AuthorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id));
            return RedirectToAction(nameof(Index));
        }    
    }
}
