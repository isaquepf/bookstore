using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasisBookstore.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Addidinrelationaltables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Livro_Forma_Pagamento",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Livro_Autor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Livro_Assunto",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Livro_Forma_Pagamento");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Livro_Autor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Livro_Assunto");
        }
    }
}
