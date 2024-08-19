using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amazon.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLaptopTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Laptops");

            migrationBuilder.AlterColumn<string>(
                name: "Images",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Laptops",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Laptops");

            migrationBuilder.AlterColumn<string>(
                name: "Images",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
