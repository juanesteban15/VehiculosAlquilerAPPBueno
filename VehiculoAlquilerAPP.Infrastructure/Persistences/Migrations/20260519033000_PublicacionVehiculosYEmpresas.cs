using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculoAlquilerAPP.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class PublicacionVehiculosYEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NitEmpresa",
                table: "Usuarios",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresa",
                table: "Usuarios",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCuenta",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Persona");

            migrationBuilder.CreateTable(
                name: "DocumentosVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoPlaca = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosVehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosVehiculo_Vehiculos_VehiculoPlaca",
                        column: x => x.VehiculoPlaca,
                        principalTable: "Vehiculos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehiculoFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoPlaca = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiculoFotos_Vehiculos_VehiculoPlaca",
                        column: x => x.VehiculoPlaca,
                        principalTable: "Vehiculos",
                        principalColumn: "Placa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosVehiculo_VehiculoPlaca",
                table: "DocumentosVehiculo",
                column: "VehiculoPlaca");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoFotos_VehiculoPlaca",
                table: "VehiculoFotos",
                column: "VehiculoPlaca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DocumentosVehiculo");
            migrationBuilder.DropTable(name: "VehiculoFotos");

            migrationBuilder.DropColumn(name: "NitEmpresa", table: "Usuarios");
            migrationBuilder.DropColumn(name: "NombreEmpresa", table: "Usuarios");
            migrationBuilder.DropColumn(name: "TipoCuenta", table: "Usuarios");
        }
    }
}
