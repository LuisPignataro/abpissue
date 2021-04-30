using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programarcr.Contabilidad.Migrations
{
    public partial class AsientosMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCAsiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodoId = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Borrador = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
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
                    table.PrimaryKey("PK_ACCAsiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ACCAsiento_ACCPeriodoContable_PeriodoId",
                        column: x => x.PeriodoId,
                        principalTable: "ACCPeriodoContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ACCLinea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsientoId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CuentaId = table.Column<string>(type: "nvarchar(21)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cambio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ACCLinea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ACCLinea_ACCAsiento_AsientoId",
                        column: x => x.AsientoId,
                        principalTable: "ACCAsiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ACCLinea_ACCCuenta_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "ACCCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ACCLinea_ACCMoneda_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "ACCMoneda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCAsiento_PeriodoId",
                table: "ACCAsiento",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_ACCLinea_AsientoId",
                table: "ACCLinea",
                column: "AsientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ACCLinea_CuentaId",
                table: "ACCLinea",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ACCLinea_MonedaId",
                table: "ACCLinea",
                column: "MonedaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCLinea");

            migrationBuilder.DropTable(
                name: "ACCAsiento");
        }
    }
}
