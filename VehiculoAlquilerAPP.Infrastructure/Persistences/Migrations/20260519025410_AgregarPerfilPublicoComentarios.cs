using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculoAlquilerAPP.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPerfilPublicoComentarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfilRuta",
                table: "Usuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComentariosUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioEvaluadoId = table.Column<int>(type: "int", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariosUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentariosUsuario_Usuarios_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComentariosUsuario_Usuarios_UsuarioEvaluadoId",
                        column: x => x.UsuarioEvaluadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentosVerificacionUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NombreArchivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RutaArchivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRevision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacionRevision = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosVerificacionUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentosVerificacionUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosUsuario_AutorId",
                table: "ComentariosUsuario",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosUsuario_UsuarioEvaluadoId",
                table: "ComentariosUsuario",
                column: "UsuarioEvaluadoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosVerificacionUsuario_UsuarioId",
                table: "DocumentosVerificacionUsuario",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentariosUsuario");

            migrationBuilder.DropTable(
                name: "DocumentosVerificacionUsuario");

            migrationBuilder.DropColumn(
                name: "FotoPerfilRuta",
                table: "Usuarios");
        }
    }
}
