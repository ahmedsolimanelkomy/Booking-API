using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_API.Migrations
{
    /// <inheritdoc />
    public partial class featureedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureHotel_Hotels_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureHotel_PhotosId",
                table: "FeatureHotel",
                column: "PhotosId");
        }
    }
}
