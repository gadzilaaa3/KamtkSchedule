using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamtkSchedule.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_v121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Building",
                schema: "dbo",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Disciplines",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building",
                schema: "dbo",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Disciplines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
