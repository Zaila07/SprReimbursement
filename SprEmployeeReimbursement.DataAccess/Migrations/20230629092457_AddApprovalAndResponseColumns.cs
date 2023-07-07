using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SprEmployeeReimbursement.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovalAndResponseColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ReimbursementModels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                table: "ReimbursementModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ReimbursementModels");

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                table: "ReimbursementModels");
        }
    }
}
