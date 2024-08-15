using System.ComponentModel.DataAnnotations;

namespace Basis.Bookstore.Api.Model
{
    public class AuthorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        [Display(Name = "Nome")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
