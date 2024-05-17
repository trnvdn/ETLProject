using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_Lib.Migrations
{
    /// <inheritdoc />
    public partial class ImplementIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CabTrip_DropoffDateTime",
                table: "CabTrips",
                column: "DropoffDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_CabTrip_PULocationID",
                table: "CabTrips",
                column: "PULocationID");

            migrationBuilder.CreateIndex(
                name: "IX_CabTrip_TripDistance",
                table: "CabTrips",
                column: "TripDistance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CabTrip_DropoffDateTime",
                table: "CabTrips");

            migrationBuilder.DropIndex(
                name: "IX_CabTrip_PULocationID",
                table: "CabTrips");

            migrationBuilder.DropIndex(
                name: "IX_CabTrip_TripDistance",
                table: "CabTrips");
        }
    }
}
