using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KamtkSchedule.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_v120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Teachers_TeacherId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "CabinetName",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "Discipline",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                schema: "dbo",
                table: "Pairs",
                newName: "DisciplineId");

            migrationBuilder.RenameIndex(
                name: "IX_Pairs_TeacherId",
                schema: "dbo",
                table: "Pairs",
                newName: "IX_Pairs_DisciplineId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "Cabinets",
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
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherPairs",
                schema: "dbo",
                columns: table => new
                {
                    PairsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPairs", x => new { x.PairsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_TeacherPairs_Pairs_PairsId",
                        column: x => x.PairsId,
                        principalSchema: "dbo",
                        principalTable: "Pairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherPairs_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairCabinets",
                schema: "dbo",
                columns: table => new
                {
                    CabinetsId = table.Column<int>(type: "int", nullable: false),
                    PairsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairCabinets", x => new { x.CabinetsId, x.PairsId });
                    table.ForeignKey(
                        name: "FK_PairCabinets_Cabinets_CabinetsId",
                        column: x => x.CabinetsId,
                        principalSchema: "dbo",
                        principalTable: "Cabinets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairCabinets_Pairs_PairsId",
                        column: x => x.PairsId,
                        principalSchema: "dbo",
                        principalTable: "Pairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupDisciplines",
                schema: "dbo",
                columns: table => new
                {
                    DisciplinesId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDisciplines", x => new { x.DisciplinesId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_GroupDisciplines_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDisciplines_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalSchema: "dbo",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDisciplines",
                schema: "dbo",
                columns: table => new
                {
                    DisciplinesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDisciplines", x => new { x.DisciplinesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_TeacherDisciplines_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalSchema: "dbo",
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDisciplines_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalSchema: "dbo",
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDisciplines_GroupsId",
                schema: "dbo",
                table: "GroupDisciplines",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_PairCabinets_PairsId",
                schema: "dbo",
                table: "PairCabinets",
                column: "PairsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDisciplines_TeachersId",
                schema: "dbo",
                table: "TeacherDisciplines",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPairs_TeachersId",
                schema: "dbo",
                table: "TeacherPairs",
                column: "TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Disciplines_DisciplineId",
                schema: "dbo",
                table: "Pairs",
                column: "DisciplineId",
                principalSchema: "dbo",
                principalTable: "Disciplines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_Disciplines_DisciplineId",
                schema: "dbo",
                table: "Pairs");

            migrationBuilder.DropTable(
                name: "GroupDisciplines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PairCabinets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TeacherDisciplines",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TeacherPairs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cabinets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Disciplines",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "DisciplineId",
                schema: "dbo",
                table: "Pairs",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Pairs_DisciplineId",
                schema: "dbo",
                table: "Pairs",
                newName: "IX_Pairs_TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Teachers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "CabinetName",
                schema: "dbo",
                table: "Pairs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discipline",
                schema: "dbo",
                table: "Pairs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_Teachers_TeacherId",
                schema: "dbo",
                table: "Pairs",
                column: "TeacherId",
                principalSchema: "dbo",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
