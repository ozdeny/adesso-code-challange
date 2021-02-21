using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdessoRideShare.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    RideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureCityId = table.Column<int>(type: "int", nullable: true),
                    ArrivalCityId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSeatCount = table.Column<int>(type: "int", nullable: false),
                    FreeSeatCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.RideId);
                    table.ForeignKey(
                        name: "FK_Rides_Cities_ArrivalCityId",
                        column: x => x.ArrivalCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Rides_Cities_DepartureCityId",
                        column: x => x.DepartureCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Rides_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideUsers",
                columns: table => new
                {
                    JoinedRidesRideId = table.Column<int>(type: "int", nullable: false),
                    JoinedUsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideUsers", x => new { x.JoinedRidesRideId, x.JoinedUsersUserId });
                    table.ForeignKey(
                        name: "FK_RideUsers_Rides_JoinedRidesRideId",
                        column: x => x.JoinedRidesRideId,
                        principalTable: "Rides",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RideUsers_Users_JoinedUsersUserId",
                        column: x => x.JoinedUsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RouteCities",
                columns: table => new
                {
                    RidesRideId = table.Column<int>(type: "int", nullable: false),
                    RouteCitiesCityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteCities", x => new { x.RidesRideId, x.RouteCitiesCityId });
                    table.ForeignKey(
                        name: "FK_RouteCities_Cities_RouteCitiesCityId",
                        column: x => x.RouteCitiesCityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RouteCities_Rides_RidesRideId",
                        column: x => x.RidesRideId,
                        principalTable: "Rides",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rides_ArrivalCityId",
                table: "Rides",
                column: "ArrivalCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_CreatedUserId",
                table: "Rides",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DepartureCityId",
                table: "Rides",
                column: "DepartureCityId");

            migrationBuilder.CreateIndex(
                name: "IX_RideUsers_JoinedUsersUserId",
                table: "RideUsers",
                column: "JoinedUsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteCities_RouteCitiesCityId",
                table: "RouteCities",
                column: "RouteCitiesCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideUsers");

            migrationBuilder.DropTable(
                name: "RouteCities");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
