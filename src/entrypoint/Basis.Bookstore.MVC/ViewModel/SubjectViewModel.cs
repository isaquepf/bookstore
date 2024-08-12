using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basis.Bookstore.Mvc.ViewModel
{
    public class SubjectViewModel
    {
        public IEnumerable<SelectListItem> Subjects { get; set; } = [];

    }
}
