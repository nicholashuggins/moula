using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    RequestStatusId = table.Column<int>(nullable: false),
                    StatusReasonId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                });
            migrationBuilder.AddForeignKey("fkeyRequestStatus", "PaymentRequests", "RequestStatusId", "RequestStatuses", null, null, "Id");
            migrationBuilder.AddForeignKey("fkeyStatusReason", "PaymentRequests", "StatusReasonId", "StatusReasons", null, null, "Id");

            migrationBuilder.InsertData(
                table: "RequestStatuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 0, "Pending" },
                    { 1, "Processed" },
                    { 2, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "StatusReasons",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 0, "" },
                    { 1, "Insufficient funds" },
                    { 2, "Payment request processed, cannot cancel" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRequests");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "StatusReasons");

            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
