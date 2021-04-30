using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programarcr.Contabilidad.Migrations
{
    public partial class TipoCambioMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCTipoCambio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonedaId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Compra = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    Venta = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCTipoCambio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "MonedaFechaIndex",
                table: "ACCTipoCambio",
                columns: new[] { "MonedaId", "Fecha" },
                unique: true,
                filter: "[MonedaId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCTipoCambio");
        }
    }
}
