using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLaptopPurchaseRelationshipCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Laptops_LaptopId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Laptops");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_LaptopId",
                table: "Order",
                newName: "IX_Order_LaptopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaptopId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_purchases_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchases_Laptops_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_purchases_CustomerId",
                table: "purchases",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_LaptopId",
                table: "purchases",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Laptops_LaptopId",
                table: "Order",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Laptops_LaptopId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_LaptopId",
                table: "Orders",
                newName: "IX_Orders_LaptopId");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Laptops",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Laptops_LaptopId",
                table: "Orders",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
