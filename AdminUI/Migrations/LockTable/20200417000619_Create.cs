using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminUI.Migrations.LockTable
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LockTableRow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimesheetID = table.Column<int>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    TimeLocked = table.Column<DateTime>(nullable: false),
                    LastInteraction = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockTableRow", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LockTableRow");
        }
    }
}
