using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeRentalApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikeStores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Surcharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwningStoreId = table.Column<int>(type: "int", nullable: false),
                    FrameSize = table.Column<int>(type: "int", nullable: false),
                    ElectricMotor = table.Column<bool>(type: "bit", nullable: true),
                    AllTerrainSuspension = table.Column<bool>(type: "bit", nullable: true),
                    BikeStyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bikes_BikeStores_OwningStoreId",
                        column: x => x.OwningStoreId,
                        principalTable: "BikeStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    SupervisorNavigationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BikeStores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "BikeStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SupervisorNavigationId",
                        column: x => x.SupervisorNavigationId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BikeId = table.Column<int>(type: "int", nullable: false),
                    CurrentStoreId = table.Column<int>(type: "int", nullable: true),
                    Archive = table.Column<bool>(type: "bit", nullable: true),
                    DateReserved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_BikeStores_CurrentStoreId",
                        column: x => x.CurrentStoreId,
                        principalTable: "BikeStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BikeStores",
                columns: new[] { "Id", "CreatedAt", "DailyRate", "Discount", "Latitude", "Longitude", "Name", "PhoneNumber", "Surcharge" },
                values: new object[] { 1, new DateTime(2021, 5, 12, 13, 49, 14, 520, DateTimeKind.Local).AddTicks(8768), 15.00m, 0.10m, 34.749432m, -77.421997m, "Larry's Bike Shop", "1-800-BIKE", 2.99m });

            migrationBuilder.InsertData(
                table: "BikeStores",
                columns: new[] { "Id", "CreatedAt", "DailyRate", "Discount", "Latitude", "Longitude", "Name", "PhoneNumber", "Surcharge" },
                values: new object[] { 2, new DateTime(2021, 5, 12, 13, 49, 14, 521, DateTimeKind.Local).AddTicks(2863), 15.00m, 0.10m, 37.749432m, -71.421997m, "Timmy Got The Wheels", "1-800-BIKE", 5.99m });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "EmailAddress", "FirstName", "LastName", "Latitude", "Longitude", "PhoneNumber" },
                values: new object[] { 1, new DateTime(2021, 5, 12, 13, 49, 14, 521, DateTimeKind.Local).AddTicks(5090), "cbernall@gmail.com", "Craig", "Bernall", null, null, "123-4567" });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "AllTerrainSuspension", "Available", "BikeStyle", "CreatedAt", "ElectricMotor", "FrameSize", "OwningStoreId", "Price" },
                values: new object[,]
                {
                    { 1, null, false, "Mountain", new DateTime(2021, 5, 12, 13, 49, 14, 517, DateTimeKind.Local).AddTicks(2801), false, 100, 1, 20.00m },
                    { 2, null, false, "Road", new DateTime(2021, 5, 12, 13, 49, 14, 519, DateTimeKind.Local).AddTicks(5014), false, 230, 1, 60.00m },
                    { 4, true, true, "Dirt", new DateTime(2021, 5, 12, 13, 49, 14, 519, DateTimeKind.Local).AddTicks(5098), true, 200, 1, 75.00m },
                    { 5, null, true, "Trike", new DateTime(2021, 5, 12, 13, 49, 14, 519, DateTimeKind.Local).AddTicks(5101), false, 75, 1, 15.00m },
                    { 3, true, true, "Road", new DateTime(2021, 5, 12, 13, 49, 14, 519, DateTimeKind.Local).AddTicks(5093), false, 45, 2, 30.00m }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "EmailAddress", "FirstName", "JobTitle", "LastName", "PhoneNumber", "StoreId", "SupervisorId", "SupervisorNavigationId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 12, 13, 49, 14, 521, DateTimeKind.Local).AddTicks(9394), "mkarepov@bikesRus.com", "Maksim", "Cashier", "Karepov", "867-5309", 1, 2, null },
                    { 2, new DateTime(2021, 5, 12, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(824), "jdopke@bikesRus.com", "Jason", "Manager", "Dopke", "555-5555", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Archive", "BikeId", "CreatedAt", "CurrentStoreId", "CustomerId", "DateDue", "DateReserved", "DateReturned", "GrandTotal" },
                values: new object[] { 1, true, 1, new DateTime(2021, 5, 12, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(3978), 1, 1, new DateTime(2021, 5, 10, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(5391), new DateTime(2021, 4, 10, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(5021), new DateTime(2021, 5, 12, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(5776), 1000.00m });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Archive", "BikeId", "CreatedAt", "CurrentStoreId", "CustomerId", "DateDue", "DateReserved", "DateReturned", "GrandTotal" },
                values: new object[] { 2, true, 2, new DateTime(2021, 5, 12, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(6963), 1, 1, new DateTime(2021, 5, 26, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(6999), new DateTime(2021, 4, 28, 13, 49, 14, 522, DateTimeKind.Local).AddTicks(6991), null, 250.00m });

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_OwningStoreId",
                table: "Bikes",
                column: "OwningStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorNavigationId",
                table: "Employees",
                column: "SupervisorNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BikeId",
                table: "Reservations",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CurrentStoreId",
                table: "Reservations",
                column: "CurrentStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "BikeStores");
        }
    }
}
