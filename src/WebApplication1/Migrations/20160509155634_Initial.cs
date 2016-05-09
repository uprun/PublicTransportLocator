using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportRoute",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportRoute", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "RoutePoint",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    NextRoutePointID = table.Column<int>(nullable: true),
                    TransportRouteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoint", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoutePoint_RoutePoint_NextRoutePointID",
                        column: x => x.NextRoutePointID,
                        principalTable: "RoutePoint",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutePoint_TransportRoute_TransportRouteID",
                        column: x => x.TransportRouteID,
                        principalTable: "TransportRoute",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "TransportLocation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<double>(nullable: false),
                    LocationRecordedTime = table.Column<DateTime>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    TransportRouteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportLocation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransportLocation_TransportRoute_TransportRouteID",
                        column: x => x.TransportRouteID,
                        principalTable: "TransportRoute",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("RoutePoint");
            migrationBuilder.DropTable("TransportLocation");
            migrationBuilder.DropTable("TransportRoute");
        }
    }
}
