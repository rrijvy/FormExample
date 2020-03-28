using Microsoft.EntityFrameworkCore.Migrations;

namespace FormExample.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryFinding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: true),
                    PrimaryFindingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryFinding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryFinding_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrimaryFinding_PrimaryFinding_PrimaryFindingId",
                        column: x => x.PrimaryFindingId,
                        principalTable: "PrimaryFinding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryFinding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PrimaryFindingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryFinding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryFinding_PrimaryFinding_PrimaryFindingId",
                        column: x => x.PrimaryFindingId,
                        principalTable: "PrimaryFinding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryFinding_PatientId",
                table: "PrimaryFinding",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryFinding_PrimaryFindingId",
                table: "PrimaryFinding",
                column: "PrimaryFindingId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryFinding_PrimaryFindingId",
                table: "SecondaryFinding",
                column: "PrimaryFindingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecondaryFinding");

            migrationBuilder.DropTable(
                name: "PrimaryFinding");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
