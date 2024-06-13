using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class featureedit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Hotels_HotelId",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_HotelId",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Feature");

            migrationBuilder.CreateTable(
                name: "FeatureHotel",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    HotelsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureHotel", x => new { x.FeaturesId, x.HotelsId });
                    table.ForeignKey(
                        name: "FK_FeatureHotel_Feature_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureHotel_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureHotel_HotelsId",
                table: "FeatureHotel",
                column: "HotelsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureHotel");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Feature",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_HotelId",
                table: "Feature",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Hotels_HotelId",
                table: "Feature",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
