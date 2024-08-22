using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLaptopCustomerRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseCount",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LaptopId",
                table: "Customers",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LaptopId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PurchaseCount",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "Customers");
        }
    }
}
