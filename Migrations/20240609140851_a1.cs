using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class a1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_Room",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "HotelWishList");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PriceRange",
                table: "Hotels");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Hotels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Hotels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WishListId",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelPhoto_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeatureHotel",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    PhotosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureHotel", x => new { x.FeaturesId, x.PhotosId });
                    table.ForeignKey(
                        name: "FK_FeatureHotel_Feature_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FeatureHotel_Hotels_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_WishListId",
                table: "Hotels",
                column: "WishListId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureHotel_PhotosId",
                table: "FeatureHotel",
                column: "PhotosId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelPhoto_HotelId",
                table: "HotelPhoto",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_WishLists_WishListId",
                table: "Hotels",
                column: "WishListId",
                principalTable: "WishLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_WishLists_WishListId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "FeatureHotel");

            migrationBuilder.DropTable(
                name: "HotelPhoto");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_WishListId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "WishListId",
                table: "Hotels");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceRange",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airlines_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HotelWishList",
                columns: table => new
                {
                    WishList = table.Column<int>(type: "int", nullable: false),
                    WishListsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelWishList", x => new { x.WishList, x.WishListsId });
                    table.ForeignKey(
                        name: "FK_HotelWishList_Hotels_WishList",
                        column: x => x.WishList,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HotelWishList_WishLists_WishListsId",
                        column: x => x.WishListsId,
                        principalTable: "WishLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineId = table.Column<int>(type: "int", nullable: true),
                    ArrivalAirport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BaggageAllowance = table.Column<int>(type: "int", nullable: true),
                    Class = table.Column<int>(type: "int", nullable: true),
                    DepartureAirport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    FlightNumber = table.Column<int>(type: "int", nullable: true),
                    StopOversNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineId",
                        column: x => x.AirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AvailabilityStatus = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Airlines_CityId",
                table: "Airlines",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineId",
                table: "Flights",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelWishList_WishListsId",
                table: "HotelWishList",
                column: "WishListsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_Room",
                table: "Rooms",
                column: "Room",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
