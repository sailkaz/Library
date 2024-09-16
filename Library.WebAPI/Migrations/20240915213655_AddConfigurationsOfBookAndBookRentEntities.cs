using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.WebAPI.Migrations
{
    public partial class AddConfigurationsOfBookAndBookRentEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRent");

            migrationBuilder.CreateTable(
                name: "BooksRentsMap",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    RentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksRentsMap", x => new { x.BookId, x.RentId });
                    table.ForeignKey(
                        name: "FK_BooksRentsMap_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksRentsMap_Rents_RentId",
                        column: x => x.RentId,
                        principalTable: "Rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksRentsMap_RentId",
                table: "BooksRentsMap",
                column: "RentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksRentsMap");

            migrationBuilder.CreateTable(
                name: "BookRent",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    RentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRent", x => new { x.BooksId, x.RentsId });
                    table.ForeignKey(
                        name: "FK_BookRent_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRent_Rents_RentsId",
                        column: x => x.RentsId,
                        principalTable: "Rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRent_RentsId",
                table: "BookRent",
                column: "RentsId");
        }
    }
}
