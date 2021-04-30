using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Programarcr.Contabilidad.Migrations
{
    public partial class CuentasMigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCCuenta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    MonedaId = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(21)", nullable: true),
                    HasChildren = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ACCCuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ACCCuenta_ACCCuenta_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ACCCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ACCCuenta_ACCMoneda_MonedaId",
                        column: x => x.MonedaId,
                        principalTable: "ACCMoneda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCCuenta_MonedaId",
                table: "ACCCuenta",
                column: "MonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_ACCCuenta_ParentId",
                table: "ACCCuenta",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCCuenta");
        }
    }
}
