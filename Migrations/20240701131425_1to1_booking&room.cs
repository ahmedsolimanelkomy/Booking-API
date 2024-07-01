using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class _1to1_bookingroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HotelBooking_HotelBookingId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelBookingId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_RoomId",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingId",
                table: "Rooms");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_RoomId",
                table: "HotelBooking",
                column: "RoomId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_RoomId",
                table: "HotelBooking");

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelBookingId",
                table: "Rooms",
                column: "HotelBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_RoomId",
                table: "HotelBooking",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HotelBooking_HotelBookingId",
                table: "Rooms",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id");
        }
    }
}
