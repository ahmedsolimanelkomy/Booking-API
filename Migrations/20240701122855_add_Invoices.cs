using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class add_Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarRentalInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    paymentStatus = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    paymentMethod = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentalInvoices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelBookingInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    paymentStatus = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    paymentMethod = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HotelBookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelBookingInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelBookingInvoices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelBookingInvoices_HotelBooking_HotelBookingId",
                        column: x => x.HotelBookingId,
                        principalTable: "HotelBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking",
                column: "HotelBookingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalInvoices_UserId",
                table: "CarRentalInvoices",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookingInvoices_HotelBookingId",
                table: "HotelBookingInvoices",
                column: "HotelBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookingInvoices_UserId",
                table: "HotelBookingInvoices",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_HotelBookingInvoices_HotelBookingInvoiceId",
                table: "HotelBooking",
                column: "HotelBookingInvoiceId",
                principalTable: "HotelBookingInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_HotelBookingInvoices_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropTable(
                name: "CarRentalInvoices");

            migrationBuilder.DropTable(
                name: "HotelBookingInvoices");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingInvoiceId",
                table: "HotelBooking");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_HotelBooking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "HotelBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);
        }
    }
}
