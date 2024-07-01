using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class BookingName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Hotels_HotelId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bookings_HotelBookingId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "HotelBooking");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "HotelBooking",
                newName: "IX_HotelBooking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "HotelBooking",
                newName: "IX_HotelBooking_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_HotelId",
                table: "HotelBooking",
                newName: "IX_HotelBooking_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CarId",
                table: "HotelBooking",
                newName: "IX_HotelBooking_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelBooking",
                table: "HotelBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_AspNetUsers_UserId",
                table: "HotelBooking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Cars_CarId",
                table: "HotelBooking",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Hotels_HotelId",
                table: "HotelBooking",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Rooms_RoomId",
                table: "HotelBooking",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_HotelBooking_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_HotelBooking_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HotelBooking_HotelBookingId",
                table: "Rooms",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_AspNetUsers_UserId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_Cars_CarId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_Hotels_HotelId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_Rooms_RoomId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_HotelBooking_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_HotelBooking_HotelBookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HotelBooking_HotelBookingId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelBooking",
                table: "HotelBooking");

            migrationBuilder.RenameTable(
                name: "HotelBooking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_HotelBooking_UserId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelBooking_RoomId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelBooking_HotelId",
                table: "Bookings",
                newName: "IX_Bookings_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelBooking_CarId",
                table: "Bookings",
                newName: "IX_Bookings_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Hotels_HotelId",
                table: "Bookings",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bookings_HotelBookingId",
                table: "Rooms",
                column: "HotelBookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
