using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaBuy.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280549d0-a41d-42e2-895e-599f191b2f71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fcefca7-d7f3-45f4-b37a-2c47268765ba", "AQAAAAEAACcQAAAAEJf0iaDzVVhw5tDBtgy9DSQblYq1rzMqedNdeN3czMM/5GqjckLsxKNghJawfQMdpg==", "28a44e4d-3c7e-4a88-a7bc-1a202c2c95c5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "280549d0-a41d-42e2-895e-599f191b2f71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46d47e89-6c71-4c84-ab5e-163f33beb408", "AQAAAAEAACcQAAAAEOidKXti0JpJ8ZZBwHTfItCWV4/6FG999x7iT+P76xkaPJtKtWQyTA8Rb3/6GZvqsg==", "e4ad3ff9-7cea-4a3c-aa1d-ad5e4a4c79c2" });
        }
    }
}
