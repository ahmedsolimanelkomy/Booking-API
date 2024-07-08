using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalInvoices_CarRentals_CarRentalId",
                table: "CarRentalInvoices");

            migrationBuilder.RenameColumn(
                name: "paymentStatus",
                table: "CarRentalInvoices",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "paymentMethod",
                table: "CarRentalInvoices",
                newName: "PaymentMethod");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "CarRentalInvoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarRentalId",
                table: "CarRentalInvoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CarRentalInvoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalInvoices_CarRentals_CarRentalId",
                table: "CarRentalInvoices",
                column: "CarRentalId",
                principalTable: "CarRentals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentalInvoices_CarRentals_CarRentalId",
                table: "CarRentalInvoices");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "CarRentalInvoices",
                newName: "paymentStatus");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "CarRentalInvoices",
                newName: "paymentMethod");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "CarRentalInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarRentalId",
                table: "CarRentalInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "CarRentalInvoices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentalInvoices_CarRentals_CarRentalId",
                table: "CarRentalInvoices",
                column: "CarRentalId",
                principalTable: "CarRentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
