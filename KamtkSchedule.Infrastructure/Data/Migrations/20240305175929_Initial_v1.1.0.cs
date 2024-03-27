using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamtkSchedule.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_v110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDays_ScheduleStaffWeeks_ScheduleStaffWeekId",
                table: "ScheduleDays");

            migrationBuilder.DropTable(
                name: "ScheduleStaffWeeks");

            migrationBuilder.DropTable(
                name: "ScheduleWeeks");

            migrationBuilder.DropColumn(
                name: "WhoHasAPair",
                table: "Pairs");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "ScheduleDays",
                newName: "ScheduleDays",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Pairs",
                newName: "Pairs",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "ScheduleStaffWeekId",
                schema: "dbo",
                table: "ScheduleDays",
                newName: "GroupScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleDays_ScheduleStaffWeekId",
                schema: "dbo",
                table: "ScheduleDays",
                newName: "IX_ScheduleDays_GroupScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "Discipline",
                schema: "dbo",
                table: "Pairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CabinetName",
                schema: "dbo",
                table: "Pairs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                schema: "dbo",
                table: "Pairs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                schema: "dbo",
                table: "Pairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building = table.Column<int>(type: "int", nullable: false),
                    DateInfo_ScheduleStartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInfo_ScheduleEndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklySchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupSchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    WeeklyScheduleId = table.Column<int>(type: "int", nullable: true),
                    DateInfo_ScheduleStartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInfo_ScheduleEndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSchedules_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupSchedules_WeeklySchedules_WeeklyScheduleId",
                        column: x => x.WeeklyScheduleId,
                        principalTable: "WeeklySchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_GroupId",
                schema: "dbo",
                table: "Pairs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_TeacherId",
                schema: "dbo",
                table: "Pairs",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSchedules_GroupId",
                schema: "dbo",
                table: "GroupSchedules",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSchedules_WeeklyScheduleId",
                schema: "dbo",
                table: "GroupSchedules",
                column: "WeeklyScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Groups_GroupId",
                schema: "dbo",
                table: "Pairs",
                column: "GroupId",
                principalSchema: "dbo",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Teachers_TeacherId",
                schema: "dbo",
                table: "Pairs",
                column: "TeacherId",
                principalSchema: "dbo",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDays_GroupSchedules_GroupScheduleId",
                schema: "dbo",
                table: "ScheduleDays",
                column: "GroupScheduleId",
                principalSchema: "dbo",
                principalTable: "GroupSchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Groups_GroupId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Teachers_TeacherId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDays_GroupSchedules_GroupScheduleId",
                schema: "dbo",
                table: "ScheduleDays");

            migrationBuilder.DropTable(
                name: "GroupSchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Teachers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WeeklySchedules");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_GroupId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_TeacherId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.RenameTable(
                name: "ScheduleDays",
                schema: "dbo",
                newName: "ScheduleDays");

            migrationBuilder.RenameTable(
                name: "Pairs",
                schema: "dbo",
                newName: "Pairs");

            migrationBuilder.RenameColumn(
                name: "GroupScheduleId",
                table: "ScheduleDays",
                newName: "ScheduleStaffWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleDays_GroupScheduleId",
                table: "ScheduleDays",
                newName: "IX_ScheduleDays_ScheduleStaffWeekId");

            migrationBuilder.AlterColumn<string>(
                name: "Discipline",
                table: "Pairs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CabinetName",
                table: "Pairs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "WhoHasAPair",
                table: "Pairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ScheduleWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DateInfo_ScheduleEndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInfo_ScheduleStartDay = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    For = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleWeekId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DateInfo_ScheduleEndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateInfo_ScheduleStartDay = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleStaffWeeks_ScheduleWeekId",
                table: "ScheduleStaffWeeks",
                column: "ScheduleWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDays_ScheduleStaffWeeks_ScheduleStaffWeekId",
                table: "ScheduleDays",
                column: "ScheduleStaffWeekId",
                principalTable: "ScheduleStaffWeeks",
                principalColumn: "Id");
        }
    }
}
