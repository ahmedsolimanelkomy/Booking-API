using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class Hotel_models_refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_WishLists_WishListId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Reviews",
                newName: "HotelBookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                newName: "IX_Reviews_HotelBookingId");

            migrationBuilder.RenameColumn(
                name: "WishListId",
                table: "Hotels",
                newName: "HotelWishListId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_WishListId",
                table: "Hotels",
                newName: "IX_Hotels_HotelWishListId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoUrl",
                table: "HotelPhoto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Hotels_WishLists_HotelWishListId",
                table: "Hotels",
                column: "HotelWishListId",
                principalTable: "WishLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Hotels_HotelId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_WishLists_HotelWishListId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_HotelBookingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "HotelBookingId",
                table: "Reviews",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_HotelBookingId",
                table: "Reviews",
                newName: "IX_Reviews_BookingId");

            migrationBuilder.RenameColumn(
                name: "HotelWishListId",
                table: "Hotels",
                newName: "WishListId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_HotelWishListId",
                table: "Hotels",
                newName: "IX_Hotels_WishListId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotoUrl",
                table: "HotelPhoto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_WishLists_WishListId",
                table: "Hotels",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
