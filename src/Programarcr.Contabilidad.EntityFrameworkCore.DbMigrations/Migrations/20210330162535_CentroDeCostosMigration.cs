using Microsoft.EntityFrameworkCore.Migrations;

namespace Programarcr.Contabilidad.Migrations
{
    public partial class CentroDeCostosMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCCentroCosto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCCentroCosto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nombre",
                table: "ACCCentroCosto",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCCentroCosto");
        }
    }
}
