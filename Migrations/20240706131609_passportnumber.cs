using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class passportnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_AspNetUsers_UserId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_UserId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PassportId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Passports",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PassportId",
                table: "AspNetUsers",
                column: "PassportId",
                unique: true,
                filter: "[PassportId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PassportId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "PassportNumber",
                table: "Passports",
                type: "int",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_UserId",
                table: "Passports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PassportId",
                table: "AspNetUsers",
                column: "PassportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_AspNetUsers_UserId",
                table: "Passports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
