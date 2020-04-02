using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminUI.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    TimesheetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(nullable: true),
                    ClientPrime = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    ProviderID = table.Column<string>(nullable: true),
                    CMOrg = table.Column<string>(nullable: true),
                    SCPAName = table.Column<string>(nullable: true),
                    Service = table.Column<string>(nullable: true),
                    ModCD = table.Column<string>(nullable: true),
                    Units = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Freq = table.Column<string>(nullable: true),
                    Hours = table.Column<double>(nullable: false),
                    ServiceGoal = table.Column<string>(nullable: true),
                    ProgressNotes = table.Column<string>(nullable: true),
                    ClientSignature = table.Column<bool>(nullable: false),
                    ClientSigned = table.Column<DateTime>(nullable: true),
                    ProviderSignature = table.Column<bool>(nullable: false),
                    ProviderSigned = table.Column<DateTime>(nullable: true),
                    Submitted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.TimesheetID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesheet");
        }
    }
}
