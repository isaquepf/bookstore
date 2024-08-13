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
            var results = _context.Database.SqlQueryRaw<ChartModel>("select * from GetBookDetailsView").ToList();
           
            return View(results);
        }      
    }
}
