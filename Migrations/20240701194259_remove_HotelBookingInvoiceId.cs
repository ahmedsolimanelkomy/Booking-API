using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class remove_HotelBookingInvoiceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_HotelBookingInvoices_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingId",
                table: "HotelBookingInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices",
                column: "HotelBookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBookingInvoices_HotelBooking_HotelBookingId",
                table: "HotelBookingInvoices",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookingInvoices_HotelBooking_HotelBookingId",
                table: "HotelBookingInvoices");

            migrationBuilder.DropIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices");

            migrationBuilder.DropColumn(
                name: "HotelBookingId",
                table: "HotelBookingInvoices");

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking",
                column: "HotelBookingInvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_HotelBookingInvoices_HotelBookingInvoiceId",
                table: "HotelBooking",
                column: "HotelBookingInvoiceId",
                principalTable: "HotelBookingInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
