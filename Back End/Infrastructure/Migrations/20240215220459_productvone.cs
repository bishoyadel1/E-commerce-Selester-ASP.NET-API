using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productvone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ScreenSize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "OSVersion",
                table: "Products",
                newName: "ProductDetailtwo");

            migrationBuilder.RenameColumn(
                name: "Camera",
                table: "Products",
                newName: "ProductDetailthree");

            migrationBuilder.RenameColumn(
                name: "CPU",
                table: "Products",
                newName: "ProductDetailsix");

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailfive",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailfour",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailone",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailsiven",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDetailfive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailfour",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailone",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductDetailsiven",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductDetailtwo",
                table: "Products",
                newName: "OSVersion");

            migrationBuilder.RenameColumn(
                name: "ProductDetailthree",
                table: "Products",
                newName: "Camera");

            migrationBuilder.RenameColumn(
                name: "ProductDetailsix",
                table: "Products",
                newName: "CPU");

            migrationBuilder.AddColumn<int>(
                name: "BatteryCapacity",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ram",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ScreenSize",
                table: "Products",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Storage",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
