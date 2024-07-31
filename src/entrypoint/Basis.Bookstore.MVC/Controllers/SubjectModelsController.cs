using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Core.Application.UseCases.Author.Create;
using Basis.Bookstore.Core.Application.UseCases.Author.Delete;
using Basis.Bookstore.Core.Application.UseCases.Author.Find;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors.Update;
using Basis.Bookstore.Core.Application.UseCases.Subject.Create;
using Basis.Bookstore.Core.Application.UseCases.Subject.Find;
using Basis.Bookstore.Core.Application.UseCases.Subject.Update;
using Basis.Bookstore.Core.Application.UseCases.Subjects.FindById;
using Basis.Bookstore.Mvc.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBook.Application.UseCases.Subject.Delete;

namespace Basis.Bookstore.Mvc.Controllers
{
    public class SubjectModelsController : Controller
    {
        private readonly IMediator _mediator;

        public SubjectModelsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: SubjectModels
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListSubjectCommand());
            return View(result.Data);
        }

        // GET: SubjectModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectModel = await _mediator.Send(new GetByIdSubjectCommand(id.Value));

            if (subjectModel.Data == null)
            {
                return NotFound();
            }

            return View(subjectModel.Data);
        }

        // GET: SubjectModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubjectModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] SubjectModel subjectModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateSubjectCommand
                {
                    Description = subjectModel.Description
                });
                return RedirectToAction(nameof(Index));
            }
            return View(subjectModel);
        }

        // GET: SubjectModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectModel = await _mediator.Send(new GetByIdSubjectCommand(id.Value));
            
            if (subjectModel.Data == null)
            {
                return NotFound();
            }
            return View(subjectModel.Data);
        }

        // POST: SubjectModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] SubjectModel subjectModel)
        {
            if (id != subjectModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateSubjectCommand
                {
                    Id = id,
                    Subject = new CreateSubjectCommand
                    {
                        Description = subjectModel.Description,
                        Id = subjectModel.Id
                    }
                });

                return RedirectToAction(nameof(Index));
            }
            return View(subjectModel);
        }

        // GET: SubjectModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectModel = await _mediator.Send(new GetByIdSubjectCommand(id.Value));

            if (subjectModel.Data == null)
            {
                return NotFound();
            }

            return View(subjectModel.Data);
        }

        // POST: SubjectModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _mediator.Send(new DeleteSubjectCommand(id));
            return RedirectToAction(nameof(Index));
        }        
    }
}
