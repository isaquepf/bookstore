using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasisBookstore.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Addpurchasemethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AuthorId",
                table: "Livro");

            migrationBuilder.DropIndex(
                name: "IX_Livro_AuthorId",
                table: "Livro");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Livro");

            migrationBuilder.InsertData(
                table: "FormaCompra",
                columns: new[] { "CodFC", "Descricao" },
                values: new object[,]
                {
                    { 1, "Balcão" },
                    { 2, "Self-service" },
                    { 3, "Internet" },
                    { 4, "Evento" },
                    { 5, "Outros" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FormaCompra",
                keyColumn: "CodFC",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormaCompra",
                keyColumn: "CodFC",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormaCompra",
                keyColumn: "CodFC",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormaCompra",
                keyColumn: "CodFC",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormaCompra",
                keyColumn: "CodFC",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Livro",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Livro_AuthorId",
                table: "Livro",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AuthorId",
                table: "Livro",
                column: "AuthorId",
                principalTable: "Autor",
                principalColumn: "CodAu");
        }
    }
}
