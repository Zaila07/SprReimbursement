using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SprEmployeeReimbursement.Migrations
{
    /// <inheritdoc />
    public partial class RenameReimbursementFolderPathToFolderPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReimbursementFolderPath",
                table: "ReimbursementModels",
                newName: "FolderPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FolderPath",
                table: "ReimbursementModels",
                newName: "ReimbursementFolderPath");
        }
    }
}
