using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class AddHotelPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhoto_Hotels_HotelId",
                table: "HotelPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelPhoto",
                table: "HotelPhoto");

            migrationBuilder.RenameTable(
                name: "HotelPhoto",
                newName: "HotelPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPhoto_HotelId",
                table: "HotelPhotos",
                newName: "IX_HotelPhotos_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelPhotos",
                table: "HotelPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhotos_Hotels_HotelId",
                table: "HotelPhotos",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelPhotos_Hotels_HotelId",
                table: "HotelPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelPhotos",
                table: "HotelPhotos");

            migrationBuilder.RenameTable(
                name: "HotelPhotos",
                newName: "HotelPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_HotelPhotos_HotelId",
                table: "HotelPhoto",
                newName: "IX_HotelPhoto_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelPhoto",
                table: "HotelPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelPhoto_Hotels_HotelId",
                table: "HotelPhoto",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
