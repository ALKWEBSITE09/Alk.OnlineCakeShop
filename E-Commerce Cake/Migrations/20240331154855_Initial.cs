using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Cake.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cakecategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakecategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cakecoupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoofCoupon = table.Column<int>(type: "int", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    MinimumAmount = table.Column<double>(type: "float", nullable: false),
                    CouponImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakecoupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cakeusertype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakeusertype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cprice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prices = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cprice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ordersstatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordersstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ordertimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordertimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceCheck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceCheck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceEnter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    priceId = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceEnter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cakesubcategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakesubcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakesubcategory_cakecategory_CategoryesId",
                        column: x => x.CategoryesId,
                        principalTable: "cakecategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cakeadmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopOpenDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShopOwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakeadmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakeadmin_cakeusertype_UserTypesId",
                        column: x => x.UserTypesId,
                        principalTable: "cakeusertype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cakeuser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakeuser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakeuser_cakeusertype_UserTypesId",
                        column: x => x.UserTypesId,
                        principalTable: "cakeusertype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cakeproduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryesId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakeproduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakeproduct_cakecategory_CategoryesId",
                        column: x => x.CategoryesId,
                        principalTable: "cakecategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cakeproduct_cakesubcategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "cakesubcategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "choices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceId = table.Column<double>(type: "float", nullable: false),
                    PriceCheckId = table.Column<int>(type: "int", nullable: false),
                    ChoiceorderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_choices_PriceCheck_PriceCheckId",
                        column: x => x.PriceCheckId,
                        principalTable: "PriceCheck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_choices_cakeuser_UserId",
                        column: x => x.UserId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_choices_ordersstatus_ChoiceorderId",
                        column: x => x.ChoiceorderId,
                        principalTable: "ordersstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cakecart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Bill = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakecart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakecart_cakeproduct_itemId",
                        column: x => x.itemId,
                        principalTable: "cakeproduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cakecart_cakeuser_UsersId",
                        column: x => x.UsersId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeId = table.Column<int>(type: "int", nullable: false),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsertId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalBill = table.Column<double>(type: "float", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inv_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inv_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inv_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inv_cakeuser_UsertId",
                        column: x => x.UsertId,
                        principalTable: "cakeuser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inv_ordersstatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "ordersstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inv_ordertimes_TimeId",
                        column: x => x.TimeId,
                        principalTable: "ordertimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cakeorderdetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    invId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cakeorderdetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cakeorderdetail_cakeproduct_itemId",
                        column: x => x.itemId,
                        principalTable: "cakeproduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cakeorderdetail_inv_invId",
                        column: x => x.invId,
                        principalTable: "inv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_CityId",
                table: "Area",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeadmin_UserTypesId",
                table: "cakeadmin",
                column: "UserTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_cakecart_itemId",
                table: "cakecart",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_cakecart_UsersId",
                table: "cakecart",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeorderdetail_invId",
                table: "cakeorderdetail",
                column: "invId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeorderdetail_itemId",
                table: "cakeorderdetail",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeproduct_CategoryesId",
                table: "cakeproduct",
                column: "CategoryesId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeproduct_SubCategoryId",
                table: "cakeproduct",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_cakesubcategory_CategoryesId",
                table: "cakesubcategory",
                column: "CategoryesId");

            migrationBuilder.CreateIndex(
                name: "IX_cakeuser_UserTypesId",
                table: "cakeuser",
                column: "UserTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_choices_ChoiceorderId",
                table: "choices",
                column: "ChoiceorderId");

            migrationBuilder.CreateIndex(
                name: "IX_choices_PriceCheckId",
                table: "choices",
                column: "PriceCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_choices_UserId",
                table: "choices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_AreaId",
                table: "inv",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_CityId",
                table: "inv",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_OrderStatusId",
                table: "inv",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_StateId",
                table: "inv",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_TimeId",
                table: "inv",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_inv_UsertId",
                table: "inv",
                column: "UsertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cakeadmin");

            migrationBuilder.DropTable(
                name: "cakecart");

            migrationBuilder.DropTable(
                name: "cakecoupon");

            migrationBuilder.DropTable(
                name: "cakeorderdetail");

            migrationBuilder.DropTable(
                name: "choices");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "Cprice");

            migrationBuilder.DropTable(
                name: "PriceEnter");

            migrationBuilder.DropTable(
                name: "cakeproduct");

            migrationBuilder.DropTable(
                name: "inv");

            migrationBuilder.DropTable(
                name: "PriceCheck");

            migrationBuilder.DropTable(
                name: "cakesubcategory");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "cakeuser");

            migrationBuilder.DropTable(
                name: "ordersstatus");

            migrationBuilder.DropTable(
                name: "ordertimes");

            migrationBuilder.DropTable(
                name: "cakecategory");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "cakeusertype");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
