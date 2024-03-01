using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaBuy.Migrations
{
    /// <inheritdoc />
    public partial class CartItemAddedUpdatedIsordredadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                table: "CartItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrdered",
                table: "CartItems");
        }
    }
}
