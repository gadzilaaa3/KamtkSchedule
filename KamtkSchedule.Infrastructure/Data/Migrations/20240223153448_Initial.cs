using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamtkSchedule.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleStaffWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    For = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleStartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleEndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleWeekId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleStaffWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleStaffWeeks_ScheduleWeeks_ScheduleWeekId",
                        column: x => x.ScheduleWeekId,
                        principalTable: "ScheduleWeeks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    ScheduleStaffWeekId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDays_ScheduleStaffWeeks_ScheduleStaffWeekId",
                        column: x => x.ScheduleStaffWeekId,
                        principalTable: "ScheduleStaffWeeks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discipline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhoHasAPair = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PairNumber = table.Column<int>(type: "int", nullable: false),
                    CabinetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleDayId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pairs_ScheduleDays_ScheduleDayId",
                        column: x => x.ScheduleDayId,
                        principalTable: "ScheduleDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_ScheduleDayId",
                table: "Pairs",
                column: "ScheduleDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleStaffWeekId",
                table: "ScheduleDays",
                column: "ScheduleStaffWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleStaffWeeks_ScheduleWeekId",
                table: "ScheduleStaffWeeks",
                column: "ScheduleWeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "ScheduleDays");

            migrationBuilder.DropTable(
                name: "ScheduleStaffWeeks");

            migrationBuilder.DropTable(
                name: "ScheduleWeeks");
        }
    }
}
