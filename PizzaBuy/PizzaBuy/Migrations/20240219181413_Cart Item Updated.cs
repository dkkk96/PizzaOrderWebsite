using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaBuy.Migrations
{
    /// <inheritdoc />
    public partial class CartItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "CartItems");
        }
    }
}
