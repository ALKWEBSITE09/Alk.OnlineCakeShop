using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Cake.Migrations
{
    /// <inheritdoc />
    public partial class Re : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "cakeuser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "feedBack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feedBack_cakeuser_UserId",
                        column: x => x.UserId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    InvId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payList_inv_InvId",
                        column: x => x.InvId,
                        principalTable: "inv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payList_ordersstatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ordersstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_review_cakeproduct_ItemId",
                        column: x => x.ItemId,
                        principalTable: "cakeproduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_review_cakeuser_UserId",
                        column: x => x.UserId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feedBack_UserId",
                table: "feedBack",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_payList_InvId",
                table: "payList",
                column: "InvId");

            migrationBuilder.CreateIndex(
                name: "IX_payList_StatusId",
                table: "payList",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_review_ItemId",
                table: "review",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_review_UserId",
                table: "review",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedBack");

            migrationBuilder.DropTable(
                name: "payList");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "cakeuser");
        }
    }
}
