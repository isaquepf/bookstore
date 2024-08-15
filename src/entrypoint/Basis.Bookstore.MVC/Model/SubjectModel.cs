using System.ComponentModel.DataAnnotations;

namespace Basis.Bookstore.Api.Model
{
    public class SubjectModel
    {
        public  int Id { get; set; }

        [Required(ErrorMessage = "A descrição do assunto é obrigatória")]
        [Display(Name = "Descrição")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
