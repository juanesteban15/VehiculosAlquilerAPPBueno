using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiculoAlquilerAPP.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class FlujoReservasNotificacionesChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    PropietarioId = table.Column<int>(type: "int", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversaciones_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conversaciones_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Conversaciones_Usuarios_PropietarioId",
                        column: x => x.PropietarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Leida = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificaciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MensajesChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversacionId = table.Column<int>(type: "int", nullable: false),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensajesChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensajesChat_Conversaciones_ConversacionId",
                        column: x => x.ConversacionId,
                        principalTable: "Conversaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensajesChat_Usuarios_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_ClienteId",
                table: "Conversaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_PropietarioId",
                table: "Conversaciones",
                column: "PropietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_ReservaId",
                table: "Conversaciones",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesChat_AutorId",
                table: "MensajesChat",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MensajesChat_ConversacionId",
                table: "MensajesChat",
                column: "ConversacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MensajesChat");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Conversaciones");
        }
    }
}
