using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basis.Bookstore.Mvc.ViewModel
{
    public class AuthorViewModel 
    {
        public IEnumerable<string> AuthorIds { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; } = [];
        
    }
}
