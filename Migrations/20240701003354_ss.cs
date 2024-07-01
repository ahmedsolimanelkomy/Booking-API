using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class ss : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingId",
                table: "HotelBooking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HotelBookingInvoice",
                table: "HotelBooking",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelBooking",
                table: "HotelBooking",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "carAgencyReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarAgencyId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carAgencyReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carAgencyReviews_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_carAgencyReviews_CarAgencies_CarAgencyId",
                        column: x => x.CarAgencyId,
                        principalTable: "CarAgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "hotelBookingInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    HotelBookingInvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelBookingInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotelBookingInvoices_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_hotelBookingInvoices_hotelBookingInvoices_HotelBookingInvoiceId",
                        column: x => x.HotelBookingInvoiceId,
                        principalTable: "hotelBookingInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "carRentalInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarRentalId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carRentalInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carRentalInvoices_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "carRentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropoffDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    AgencyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carRentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carRentals_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carRentals_CarAgencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "CarAgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_carRentals_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_carRentals_carRentalInvoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "carRentalInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_HotelBookingId",
                table: "HotelBooking",
                column: "HotelBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBooking_HotelBookingInvoice",
                table: "HotelBooking",
                column: "HotelBookingInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_carAgencyReviews_ApplicationUserID",
                table: "carAgencyReviews",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_carAgencyReviews_CarAgencyId",
                table: "carAgencyReviews",
                column: "CarAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_carRentalInvoices_ApplicationUserID",
                table: "carRentalInvoices",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_carRentalInvoices_CarRentalId",
                table: "carRentalInvoices",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_carRentals_AgencyID",
                table: "carRentals",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_carRentals_ApplicationUserID",
                table: "carRentals",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_carRentals_CarID",
                table: "carRentals",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_carRentals_InvoiceID",
                table: "carRentals",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_hotelBookingInvoices_ApplicationUserId",
                table: "hotelBookingInvoices",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelBookingInvoices_HotelBookingInvoiceId",
                table: "hotelBookingInvoices",
                column: "HotelBookingInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_AspNetUsers_HotelBookingInvoice",
                table: "HotelBooking",
                column: "HotelBookingInvoice",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_AspNetUsers_UserId",
                table: "HotelBooking",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Cars_CarId",
                table: "HotelBooking",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_HotelBooking_HotelBookingId",
                table: "HotelBooking",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Hotels_HotelId",
                table: "HotelBooking",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBooking_Rooms_RoomId",
                table: "HotelBooking",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_HotelBooking_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_HotelBooking_HotelBookingId",
                table: "Reviews",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HotelBooking_HotelBookingId",
                table: "Rooms",
                column: "HotelBookingId",
                principalTable: "HotelBooking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_carRentalInvoices_carRentals_CarRentalId",
                table: "carRentalInvoices",
                column: "CarRentalId",
                principalTable: "carRentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_AspNetUsers_HotelBookingInvoice",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_AspNetUsers_UserId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_Cars_CarId",
                table: "HotelBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBooking_HotelBooking_HotelBookingId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_carRentalInvoices_carRentals_CarRentalId",
                table: "carRentalInvoices");

            migrationBuilder.DropTable(
                name: "carAgencyReviews");

            migrationBuilder.DropTable(
                name: "hotelBookingInvoices");

            migrationBuilder.DropTable(
                name: "carRentals");

            migrationBuilder.DropTable(
                name: "carRentalInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelBooking",
                table: "HotelBooking");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_HotelBookingId",
                table: "HotelBooking");

            migrationBuilder.DropIndex(
                name: "IX_HotelBooking_HotelBookingInvoice",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingId",
                table: "HotelBooking");

            migrationBuilder.DropColumn(
                name: "HotelBookingInvoice",
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
