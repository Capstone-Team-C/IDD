using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminUI.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormType",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "ServiceAuth",
                table: "Timesheet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormType",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceAuth",
                table: "Timesheet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
