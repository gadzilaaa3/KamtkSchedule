using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamtkSchedule.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                table: "ScheduleStaffWeeks");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ScheduleStaffWeeks");

            migrationBuilder.RenameColumn(
                name: "ScheduleStartDay",
                table: "ScheduleStaffWeeks",
                newName: "DateInfo_ScheduleStartDay");

            migrationBuilder.RenameColumn(
                name: "ScheduleEndDay",
                table: "ScheduleStaffWeeks",
                newName: "DateInfo_ScheduleEndDay");

            migrationBuilder.AddColumn<int>(
                name: "Building",
                table: "ScheduleWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInfo_ScheduleEndDay",
                table: "ScheduleWeeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInfo_ScheduleStartDay",
                table: "ScheduleWeeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "ScheduleWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                table: "ScheduleWeeks");

            migrationBuilder.DropColumn(
                name: "DateInfo_ScheduleEndDay",
                table: "ScheduleWeeks");

            migrationBuilder.DropColumn(
                name: "DateInfo_ScheduleStartDay",
                table: "ScheduleWeeks");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "ScheduleWeeks");

            migrationBuilder.RenameColumn(
                name: "DateInfo_ScheduleStartDay",
                table: "ScheduleStaffWeeks",
                newName: "ScheduleStartDay");

            migrationBuilder.RenameColumn(
                name: "DateInfo_ScheduleEndDay",
                table: "ScheduleStaffWeeks",
                newName: "ScheduleEndDay");

            migrationBuilder.AddColumn<int>(
                name: "Building",
                table: "ScheduleStaffWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "ScheduleStaffWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
