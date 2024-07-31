using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Core.Application.UseCases.Author.Find;
using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Book.Create;
using Basis.Bookstore.Core.Application.UseCases.Books.Delete;
using Basis.Bookstore.Core.Application.UseCases.Books.Find;
using Basis.Bookstore.Core.Application.UseCases.Books.FindById;
using Basis.Bookstore.Core.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basis.Bookstore.Mvc.Controllers
{
    public class BookModelsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFillDataService _fillDataService;

        public BookModelsController(IMediator mediator, IFillDataService fillDataService)
        {
            _mediator = mediator;
            _fillDataService = fillDataService;
        }

        // GET: BookModels
        public async Task<IActionResult> Index()
        {                     
            var result = await _mediator.Send(new ListBookCommand());
            return View(result.Data);
        }

        // GET: BookModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorModel = await _mediator.Send(new FindByIdBookCommand(id.Value));

            if (authorModel.Data == null)
            {
                return NotFound();
            }

            return View(authorModel.Data);
        }

        // GET: BookModels/Create
        public IActionResult Create()
        {
            var authors = _fillDataService.GetAllAuthors();
            var subjects = _fillDataService.GetAllSubjects();

            var newBook = new BookModel
            {
                AuthorsIds = authors.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                SubjectsIds = subjects.Select(x => new SelectListItem { Text = x.Description, Value = x.Id.ToString() }).ToList()
            };

            return View(newBook);
        }

        // POST: BookModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Publisher,Edition,PublishedYear,AuthorsIds,SubjectsIds")] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateBookCommand
                {
                    Edition = bookModel.Edition,
                    Description = bookModel.Description,
                    PublishedAt = bookModel.PublishedYear,
                    Publisher = bookModel.Publisher,
                    AuthorsIds = bookModel.AuthorsIds.Select(x => Convert.ToInt32(x.Value)).ToList(),
                    SubjectsIds = bookModel.SubjectsIds.Select(x => Convert.ToInt32(x.Value)).ToList(),
                    Id = bookModel.Id,
                    Title = bookModel.Title,
                    
                });

                return RedirectToAction(nameof(Index));
            }
            return View(bookModel);
        }

        // GET: BookModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new FindByIdBookCommand(id.Value));


            if (result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: BookModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Publisher,Edition,PublishedYear,AuthorsIds,SubjectsIds")] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateBookCommand
                {
                    Id = bookModel.Id,
                    Description = bookModel.Description,
                    Edition = bookModel.Edition,
                    PublishedAt = bookModel.PublishedYear,
                    Publisher = bookModel.Publisher,
                    SubjectsIds = bookModel.SubjectsIds.Select(x => Convert.ToInt32(x.Value)).ToList(),
                    Title = bookModel.Title,                    
                    AuthorsIds = bookModel.AuthorsIds.Select(x => Convert.ToInt32(x.Value)).ToList(),
                    PurchaseMethods = bookModel.PurchaseMethods.Select(p => new Core.Application.UseCases.PurchaseMethods.Create.CreatePurchaseMethodCommand
                    {
                        Id = p.Id,
                        Description = p.Name,
                        Price = p.Price,
                        BookId = id
                    }).ToList(),
                });

                return RedirectToAction(nameof(Index));
            }

            return View(bookModel);            
        }

        // GET: BookModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new FindByIdBookCommand(id.Value));


            if (result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: BookModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));            
            return RedirectToAction(nameof(Index));
        }   
    }
}
