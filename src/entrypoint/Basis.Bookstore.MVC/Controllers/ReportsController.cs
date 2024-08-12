using Basis.Bookstore.Core.Domain.Contracts;
using BasisBookstore.Infraestructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Basis.Bookstore.Mvc.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult Index([FromServices] BookstoreContext context)
        {
            var results = context.Database.SqlQueryRaw<BookDto>("select * from GetBookDetailsView");


                       
            return View();
        }
    }
}
