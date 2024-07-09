using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Cake.Migrations
{
    /// <inheritdoc />
    public partial class mmmm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "subTotal",
                table: "cakecart",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "subTotal",
                table: "cakecart");
        }
    }
}
