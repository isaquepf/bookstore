using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasisBookstore.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assunto",
                columns: table => new
                {
                    CodAs = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assunto", x => x.CodAs);
                });

            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    CodAu = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.CodAu);
                });

            migrationBuilder.CreateTable(
                name: "FormaCompra",
                columns: table => new
                {
                    CodFC = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaCompra", x => x.CodFC);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Codl = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Editora = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    Edicao = table.Column<int>(type: "INTEGER", nullable: false),
                    PublishedYear = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Codl);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Assunto",
                        principalColumn: "CodAs");
                    table.ForeignKey(
                        name: "FK_Livro_Autor_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Autor",
                        principalColumn: "CodAu");
                });

            migrationBuilder.CreateTable(
                name: "Livro_Assunto",
                columns: table => new
                {
                    Codl = table.Column<int>(type: "INTEGER", nullable: false),
                    CodAs = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Assunto", x => new { x.Codl, x.CodAs });
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Assunto_CodAs",
                        column: x => x.CodAs,
                        principalTable: "Assunto",
                        principalColumn: "CodAs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Assunto_Livro_Codl",
                        column: x => x.Codl,
                        principalTable: "Livro",
                        principalColumn: "Codl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Autor",
                columns: table => new
                {
                    Codl = table.Column<int>(type: "INTEGER", nullable: false),
                    CodAu = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Autor", x => new { x.Codl, x.CodAu });
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Autor_CodAu",
                        column: x => x.CodAu,
                        principalTable: "Autor",
                        principalColumn: "CodAu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_Livro_Codl",
                        column: x => x.Codl,
                        principalTable: "Livro",
                        principalColumn: "Codl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Livro_Forma_Pagamento",
                columns: table => new
                {
                    Codl = table.Column<int>(type: "INTEGER", nullable: false),
                    CodFC = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro_Forma_Pagamento", x => new { x.Codl, x.CodFC });
                    table.ForeignKey(
                        name: "FK_Livro_Forma_Pagamento_FormaCompra_CodFC",
                        column: x => x.CodFC,
                        principalTable: "FormaCompra",
                        principalColumn: "CodFC",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Forma_Pagamento_Livro_Codl",
                        column: x => x.Codl,
                        principalTable: "Livro",
                        principalColumn: "Codl",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_AuthorId",
                table: "Livro",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_SubjectId",
                table: "Livro",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Assunto_CodAs",
                table: "Livro_Assunto",
                column: "CodAs");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Autor_CodAu",
                table: "Livro_Autor",
                column: "CodAu");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Forma_Pagamento_CodFC",
                table: "Livro_Forma_Pagamento",
                column: "CodFC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro_Assunto");

            migrationBuilder.DropTable(
                name: "Livro_Autor");

            migrationBuilder.DropTable(
                name: "Livro_Forma_Pagamento");

            migrationBuilder.DropTable(
                name: "FormaCompra");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Assunto");

            migrationBuilder.DropTable(
                name: "Autor");
        }
    }
}
