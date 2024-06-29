using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_HotelBookingId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "HotelBookingId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                unique: true,
                filter: "[HotelBookingId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_HotelBookingId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "HotelBookingId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
