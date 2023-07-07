using Microsoft.EntityFrameworkCore.Migrations;

namespace SprEmployeeReimbursement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReimbursementTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ReimbursementModels",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type='Food' WHERE Type = 0");
            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type='Medical' WHERE Type =1");
            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type='Transportation' WHERE Type =2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type = 0 WHERE Type = 'Food'");
            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type = 1 WHERE Type = 'Medical'");
            migrationBuilder.Sql("UPDATE ReimbursementModels SET Type = 2 WHERE Type = 'Transportation'");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ReimbursementModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);
        }
    }
}
