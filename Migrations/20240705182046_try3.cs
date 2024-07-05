using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class try3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_CarAgencies_CarAgencyId",
                table: "CarRentals");

            migrationBuilder.DropTable(
                name: "CarCarRental");

            migrationBuilder.DropIndex(
                name: "IX_CarRentalInvoices_CarRentalId",
                table: "CarRentalInvoices");

            migrationBuilder.AlterColumn<int>(
                name: "CarAgencyId",
                table: "CarRentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "CarRentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarRentalInvoiceId",
                table: "CarRentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CarRentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "CarAgencyReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CarId",
                table: "CarRentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_CarRentalInvoiceId",
                table: "CarRentals",
                column: "CarRentalInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_UserId",
                table: "CarRentals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalInvoices_CarRentalId",
                table: "CarRentalInvoices",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarAgencyReviews_CarId",
                table: "CarAgencyReviews",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarAgencyReviews_Cars_CarId",
                table: "CarAgencyReviews",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_AspNetUsers_UserId",
                table: "CarRentals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_CarAgencies_CarAgencyId",
                table: "CarRentals",
                column: "CarAgencyId",
                principalTable: "CarAgencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_CarRentalInvoices_CarRentalInvoiceId",
                table: "CarRentals",
                column: "CarRentalInvoiceId",
                principalTable: "CarRentalInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Cars_CarId",
                table: "CarRentals",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarAgencyReviews_Cars_CarId",
                table: "CarAgencyReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_AspNetUsers_UserId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_CarAgencies_CarAgencyId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_CarRentalInvoices_CarRentalInvoiceId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Cars_CarId",
                table: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_CarId",
                table: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_CarRentalInvoiceId",
                table: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_UserId",
                table: "CarRentals");

            migrationBuilder.DropIndex(
                name: "IX_CarRentalInvoices_CarRentalId",
                table: "CarRentalInvoices");

            migrationBuilder.DropIndex(
                name: "IX_CarAgencyReviews_CarId",
                table: "CarAgencyReviews");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "CarRentalInvoiceId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "CarAgencyReviews");

            migrationBuilder.AlterColumn<int>(
                name: "CarAgencyId",
                table: "CarRentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CarCarRental",
                columns: table => new
                {
                    CarRentalsId = table.Column<int>(type: "int", nullable: false),
                    CarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCarRental", x => new { x.CarRentalsId, x.CarsId });
                    table.ForeignKey(
                        name: "FK_CarCarRental_CarRentals_CarRentalsId",
                        column: x => x.CarRentalsId,
                        principalTable: "CarRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarCarRental_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalInvoices_CarRentalId",
                table: "CarRentalInvoices",
                column: "CarRentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarCarRental_CarsId",
                table: "CarCarRental",
                column: "CarsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_CarAgencies_CarAgencyId",
                table: "CarRentals",
                column: "CarAgencyId",
                principalTable: "CarAgencies",
                principalColumn: "Id");
        }
    }
}
