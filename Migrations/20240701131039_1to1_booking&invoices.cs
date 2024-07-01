using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class _1to1_bookinginvoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_HotelBookingInvoices_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices",
                column: "HotelBookingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices");

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices",
                column: "HotelBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking",
                column: "HotelBookingInvoiceId");

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
