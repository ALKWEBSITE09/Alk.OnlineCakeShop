using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Cake.Migrations
{
    /// <inheritdoc />
    public partial class uuu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "subId",
                table: "choices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_choices_subId",
                table: "choices",
                column: "subId");

            migrationBuilder.AddForeignKey(
                name: "FK_choices_cakesubcategory_subId",
                table: "choices",
                column: "subId",
                principalTable: "cakesubcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_choices_cakesubcategory_subId",
                table: "choices");

            migrationBuilder.DropIndex(
                name: "IX_choices_subId",
                table: "choices");

            migrationBuilder.DropColumn(
                name: "subId",
                table: "choices");
        }
    }
}
