using System;
using SprEmployeeReimbursement.DataAccess.Models;
using SprEmployeeReimbursement.DataAccess.SprDbContext;
using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace SprEmployeeReimbursement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SprEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReimbursementModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ReceiptImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SprEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReimbursementModels_SprEmployees_SprEmployeeId",
                        column: x => x.SprEmployeeId,
                        principalTable: "SprEmployees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReimbursementModels_SprEmployeeId",
                table: "ReimbursementModels",
                column: "SprEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReimbursementModels");

            migrationBuilder.DropTable(
                name: "SprEmployees");
        }
    }
}
