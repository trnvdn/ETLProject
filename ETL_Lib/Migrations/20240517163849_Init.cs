using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL_Lib.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CabTrips",
                columns: table => new
                {
                    TripID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PickupDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropoffDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    TripDistance = table.Column<double>(type: "float", nullable: false),
                    StoreAndFwdFlag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PULocationID = table.Column<int>(type: "int", nullable: false),
                    DOLocationID = table.Column<int>(type: "int", nullable: false),
                    FareAmount = table.Column<double>(type: "float", nullable: false),
                    TipAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabTrips", x => x.TripID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabTrips");
        }
    }
}
