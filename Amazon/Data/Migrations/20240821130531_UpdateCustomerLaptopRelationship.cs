using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerLaptopRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "Customers",
                newName: "NewLaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_LaptopId",
                table: "Customers",
                newName: "IX_Customers_NewLaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Laptops_NewLaptopId",
                table: "Customers",
                column: "NewLaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Laptops_NewLaptopId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "NewLaptopId",
                table: "Customers",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_NewLaptopId",
                table: "Customers",
                newName: "IX_Customers_LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Laptops_LaptopId",
                table: "Customers",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
