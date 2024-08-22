using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Laptops_NewLaptopId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_purchases_Customers_CustomerId",
                table: "purchases");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_purchases_CustomerId",
                table: "purchases");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "purchases");

            migrationBuilder.RenameColumn(
                name: "NewLaptopId",
                table: "Customers",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_NewLaptopId",
                table: "Customers",
                newName: "IX_Customers_LaptopId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Laptops",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_CustomerId",
                table: "Laptops",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Customers_CustomerId",
                table: "Laptops",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Customers_CustomerId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_CustomerId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Laptops");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "Customers",
                newName: "NewLaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_LaptopId",
                table: "Customers",
                newName: "IX_Customers_NewLaptopId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaptopId = table.Column<int>(type: "int", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Laptops_LaptopId",
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
                name: "IX_Order_LaptopId",
                table: "Order",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Laptops_NewLaptopId",
                table: "Customers",
                column: "NewLaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchases_Customers_CustomerId",
                table: "purchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
