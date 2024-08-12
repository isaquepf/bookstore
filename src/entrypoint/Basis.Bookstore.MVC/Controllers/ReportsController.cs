using Basis.Bookstore.Core.Domain.Contracts;
using Basis.Bookstore.Mvc.Models;
using BasisBookstore.Infraestructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Basis.Bookstore.Mvc.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BookstoreContext _context;

        public ReportsController(BookstoreContext context)
        {
            _context = context;
        }


        public IActionResult Index([FromServices] BookstoreContext context)
        {
            var results = _context.Database.SqlQueryRaw<BookDto>("select * from GetBookDetailsView");
            return View();
        }


        [HttpGet()]
        [Route("/Reports/getReportPrices")]
        public JsonResult GetReportByPrices()
        {
            var results = _context.Database.SqlQueryRaw<ChartDto>(@"select
                            Autor as Labels,
                            Count(*) as Data
                        from GetBookDetailsView as v
                        ");


            return Json(results);
        }
    }
}
