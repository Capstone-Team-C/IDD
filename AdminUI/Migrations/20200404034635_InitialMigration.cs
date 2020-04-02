using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminUI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormType",
                table: "Timesheet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceAuth",
                table: "Timesheet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormType",
                table: "Timesheet");

            migrationBuilder.DropColumn(
                name: "ServiceAuth",
                table: "Timesheet");
        }
    }
}
