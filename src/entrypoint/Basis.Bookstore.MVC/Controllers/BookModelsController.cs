using Basis.Bookstore.Api.Model;
using Basis.Bookstore.Core.Application.UseCases.Book.Create;
using Basis.Bookstore.Core.Application.UseCases.Books;
using Basis.Bookstore.Core.Application.UseCases.Books.Delete;
using Basis.Bookstore.Core.Application.UseCases.Books.Find;
using Basis.Bookstore.Core.Application.UseCases.Books.FindById;
using Basis.Bookstore.Core.Service;
using Basis.Bookstore.Mvc.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBook.Application.UseCases.Book.Update;

namespace Basis.Bookstore.Mvc.Controllers
{
    public class BookModelsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IFillDataService _fillDataService;

        private const string BindMapping = "Id,Title,Description,Publisher,Edition,PublishedYear,AuthorIds,SubjectIds,PurchaseItems";


        public BookModelsController(IMediator mediator, IFillDataService fillDataService)
        {
            _mediator = mediator;
            _fillDataService = fillDataService;
        }

        // GET: BookModels
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ListBookCommand());
            var authors = _fillDataService.GetAllAuthors();

            var authorViewModel = new AuthorViewModel()
            {
                Authors = authors.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };

            ViewBag.Authors = authors.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

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
            BookModel newBook = GetViewModelData();

            return View(newBook);
        }

        private BookModel GetViewModelData()
        {
            var authors = _fillDataService.GetAllAuthors();
            var subjects = _fillDataService.GetAllSubjects();
            var purchaseMethods = _fillDataService.GetAllPurchaseMethods();

            var authorViewModel = new AuthorViewModel()
            {
                Authors = authors.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            };

            var subjectViewModel = new SubjectViewModel()
            {
                Subjects = subjects.Select(x => new SelectListItem(x.Description, x.Id.ToString()))
            };

            var purchaseMethodsViewModel = new PurchaseMethodViewModel
            {
                PurchaseMethods = purchaseMethods.Select(x => new PurchaseMethodViewItemModel(x.Id, x.Name)).ToList()
            };

            var newBook = new BookModel
            {
                AuthorVM = authorViewModel,
                SubjectVM = subjectViewModel,
                PurchaseMethodsVM = purchaseMethodsViewModel,
                PurchaseItems = purchaseMethodsViewModel.PurchaseMethods
            };
            return newBook;
        }

        // POST: BookModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(BindMapping)] BookModel bookModel)
        {

            if (bookModel.AuthorIds == null || !bookModel.AuthorIds.Any())
            {
                ModelState.AddModelError("Authors", "* Ao menos um autor deve ser selecionado.");
            }

            var vm = GetViewModelData();
            bookModel.AuthorVM = vm.AuthorVM;
            bookModel.PurchaseMethodsVM = vm.PurchaseMethodsVM;
            bookModel.SubjectVM = vm.SubjectVM;

            RemoveVMValidations();

            if (ModelState.IsValid)
            {
                var purchaseMethods = bookModel.PurchaseItems.Select(p => new Core.Application.UseCases.PurchaseMethods.Create.CreatePurchaseMethodCommand()
                {
                    Id = p.Id,
                    Price = p.Price,
                }).ToList();

                var result = await _mediator.Send(new CreateBookCommand
                {
                    Edition = bookModel.Edition,
                    Description = bookModel.Description,
                    PublishedAt = bookModel.PublishedYear,
                    Publisher = bookModel.Publisher,
                    AuthorsIds = bookModel.AuthorIds.Select(x => Convert.ToInt32(x)).ToList(),
                    SubjectsIds = bookModel.SubjectIds.Select(x => Convert.ToInt32(x)).ToList(),
                    PurchaseMethods = purchaseMethods,
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

            var vm = GetViewModelData();

            var data = (BookResult)result.Data;

            var purchaseMethodsViewModel = new PurchaseMethodViewModel
            {
                PurchaseMethods = data.PurchaseMethods.Select(x => new PurchaseMethodViewItemModel(x.Id, x.Description, x.Price)).ToList()
            };


            var book = new BookModel
            {
                Id = id.Value,
                Title = data.Title,
                Description = data.Description,
                Edition = data.Edition,
                PublishedYear = data.PublishedYear,
                Publisher = data.Publisher,
                AuthorVM = vm.AuthorVM,
                SubjectVM = vm.SubjectVM,
                PurchaseMethodsVM = purchaseMethodsViewModel,
                PurchaseItems = purchaseMethodsViewModel.PurchaseMethods,
                AuthorIds = data.Authors.Select(p => p.Id.ToString()).ToList(),
                SubjectIds = data.Subjects.Select(p => p.Id.ToString()).ToList()
                //PurchaseMethodsIds = purchaseMethodsViewModel.PurchaseMethods.Select(p => p.Id.ToString()).ToList(),
            };


            if (result.Data == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(BindMapping)] BookModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            var vm = GetViewModelData();
            bookModel.AuthorVM = vm.AuthorVM;
            bookModel.PurchaseMethodsVM = vm.PurchaseMethodsVM;
            bookModel.SubjectVM = vm.SubjectVM;

            RemoveVMValidations();

            if (ModelState.IsValid)
            {
                var purchaseMethods = bookModel.PurchaseItems.Select(p => new Core.Application.UseCases.PurchaseMethods.Create.CreatePurchaseMethodCommand()
                {
                    Id = p.Id,
                    Price = p.Price,
                }).ToList();

                var result = await _mediator.Send(new UpdateBookCommand(id, new CreateBookCommand
                {
                    Id = bookModel.Id,
                    Description = bookModel.Description,
                    Edition = bookModel.Edition,
                    PublishedAt = bookModel.PublishedYear,
                    Publisher = bookModel.Publisher,
                    SubjectsIds = bookModel.SubjectIds.Select(x => Convert.ToInt32(x)).ToList(),
                    Title = bookModel.Title,
                    AuthorsIds = bookModel.AuthorIds.Select(x => Convert.ToInt32(x)).ToList(),
                    PurchaseMethods = purchaseMethods,
                }));

                return RedirectToAction(nameof(Index));
            }

            return View(bookModel);
        }

        private void RemoveVMValidations()
        {
            ModelState.Remove("PurchaseMethodsVM");
            ModelState.Remove("PurchaseItems");
            ModelState.Remove("AuthorVM");
            ModelState.Remove("SubjectVM");
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


            var data = (BookResult)result.Data;

            var purchaseMethodsViewModel = new PurchaseMethodViewModel
            {
                PurchaseMethods = data.PurchaseMethods.Select(x => new PurchaseMethodViewItemModel(x.Id, x.Description, x.Price)).ToList()
            };


            var book = new BookModel
            {
                Id = id.Value,
                Title = data.Title,
                Description = data.Description,
                Edition = data.Edition,
                PublishedYear = data.PublishedYear,
                Publisher = data.Publisher,                
                PurchaseMethodsVM = purchaseMethodsViewModel,
                PurchaseItems = purchaseMethodsViewModel.PurchaseMethods,
                AuthorIds = data.Authors.Select(p => p.Id.ToString()).ToList(),
                SubjectIds = data.Subjects.Select(p => p.Id.ToString()).ToList(),
                //PurchaseMethodsIds = purchaseMethodsViewModel.PurchaseMethods.Select(p => p.Id.ToString()).ToList(),
            };

            return View(book);
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
