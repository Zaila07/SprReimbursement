using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SprEmployeeReimbursement.Migrations
{
    /// <inheritdoc />
    public partial class AddReimbursementFolderPathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "ReimbursementFolderPath",
             table: "ReimbursementModels",
             type: "nvarchar(max)",
             nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "ReimbursementFolderPath",
            table: "ReimbursementModels");
           
        }
    }
}
