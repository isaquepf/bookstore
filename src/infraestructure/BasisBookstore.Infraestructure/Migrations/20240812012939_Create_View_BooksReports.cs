using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasisBookstore.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_BooksReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view if not exists GetBookDetailsView
                                    as
                                    select
                                        LL.Codl,
                                        LL.Titulo,
                                        A.Nome as Autor,
                                        A2.Descricao as Assunto,
                                        FC.Descricao as FormaCobranca,
                                        ll.PublishedYear as DataPublicacao,
                                        lfp.Preco
                                    from Livro LL
                                        inner join main.Livro_Autor LA on LL.Codl = LA.Codl
                                        inner join main.Autor A on A.CodAu = LA.CodAu
                                        left join main.Livro_Assunto livro_assunto on LL.Codl = livro_assunto.Codl
                                        left join main.Assunto A2 on A2.CodAs = livro_assunto.CodAs
                                        left join main.Livro_Forma_Pagamento LFP on LL.Codl = LFP.Codl
                                        left join main.FormaCompra FC on LFP.CodFC = FC.CodFC
                                    group by
                                        a.Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view GetBookDetailsView");
        }
    }
}
