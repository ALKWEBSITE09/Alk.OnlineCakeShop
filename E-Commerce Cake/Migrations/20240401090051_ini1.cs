using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Cake.Migrations
{
    /// <inheritdoc />
    public partial class ini1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "favo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favo_cakeproduct_itemId",
                        column: x => x.itemId,
                        principalTable: "cakeproduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favo_cakeuser_UsersId",
                        column: x => x.UsersId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_favo_itemId",
                table: "favo",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_favo_UsersId",
                table: "favo",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favo");
        }
    }
}
