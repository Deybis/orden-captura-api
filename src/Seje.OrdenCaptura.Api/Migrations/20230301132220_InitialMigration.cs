using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seje.OrdenCaptura.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuraciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuraciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expedientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroExpediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganoJurisdiccionalId = table.Column<int>(type: "int", nullable: false),
                    OrganoJurisdiccionalDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcedenciaId = table.Column<int>(type: "int", nullable: false),
                    ProcedenciaDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcesoId = table.Column<int>(type: "int", nullable: false),
                    ProcesoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    MateriaDescripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FechaRecepcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firmantes",
                columns: table => new
                {
                    FirmanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CoordX = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    CoordY = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Ancho = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Alto = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmantes", x => x.FirmanteId);
                });

            migrationBuilder.CreateTable(
                name: "OrdenCapturaEstados",
                columns: table => new
                {
                    OrdenCapturaEstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenCapturaEstados", x => x.OrdenCapturaEstadoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentos",
                columns: table => new
                {
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumentos", x => x.TipoDocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "TipoFirmas",
                columns: table => new
                {
                    TipoFirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoFirmas", x => x.TipoFirmaId);
                });

            migrationBuilder.CreateTable(
                name: "Delitos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelitoId = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpedienteId = table.Column<long>(type: "bigint", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delitos_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partes",
                columns: table => new
                {
                    ParteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoParteId = table.Column<int>(type: "int", nullable: false),
                    TipoParteDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SexoId = table.Column<int>(type: "int", nullable: false),
                    SexoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    PaisDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoIdentificacionId = table.Column<int>(type: "int", nullable: false),
                    TipoIdentificacionDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPersonaId = table.Column<int>(type: "int", nullable: false),
                    TipoPersonaDescripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ExpedienteId = table.Column<long>(type: "bigint", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partes", x => x.ParteId);
                    table.ForeignKey(
                        name: "FK_Partes_Expedientes_ExpedienteId",
                        column: x => x.ExpedienteId,
                        principalTable: "Expedientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesCaptura",
                columns: table => new
                {
                    OrdenCapturaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganoJurisdiccionalId = table.Column<int>(type: "int", nullable: false),
                    OrdenCapturaCodigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroOrdenCaptura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correlativo = table.Column<int>(type: "int", nullable: false),
                    OrdenCapturaEstadoId = table.Column<int>(type: "int", nullable: false),
                    OrdenCapturaEstadoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpedienteId = table.Column<long>(type: "bigint", nullable: false),
                    NumeroExpediente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstanciaId = table.Column<int>(type: "int", nullable: false),
                    InstanciaDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoSecretario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoEscribiente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoJuez = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlertaInternacional = table.Column<bool>(type: "bit", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false),
                    MunicipioDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesCaptura", x => x.OrdenCapturaId);
                    table.ForeignKey(
                        name: "FK_OrdenesCaptura_OrdenCapturaEstados_OrdenCapturaEstadoId",
                        column: x => x.OrdenCapturaEstadoId,
                        principalTable: "OrdenCapturaEstados",
                        principalColumn: "OrdenCapturaEstadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    DocumentoId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenCapturaId = table.Column<long>(type: "bigint", nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    NumeroOrdenCaptura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoMedia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Peso = table.Column<long>(type: "bigint", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.DocumentoId);
                    table.ForeignKey(
                        name: "FK_Documentos_OrdenesCaptura_OrdenCapturaId",
                        column: x => x.OrdenCapturaId,
                        principalTable: "OrdenesCaptura",
                        principalColumn: "OrdenCapturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_TipoDocumentos_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumentos",
                        principalColumn: "TipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firmas",
                columns: table => new
                {
                    FirmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoFirmaId = table.Column<int>(type: "int", nullable: false),
                    OrdenCapturaId = table.Column<long>(type: "bigint", nullable: false),
                    NumeroOrdenCaptura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoFirmante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firmo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmas", x => x.FirmaId);
                    table.ForeignKey(
                        name: "FK_Firmas_OrdenesCaptura_OrdenCapturaId",
                        column: x => x.OrdenCapturaId,
                        principalTable: "OrdenesCaptura",
                        principalColumn: "OrdenCapturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Firmas_TipoFirmas_TipoFirmaId",
                        column: x => x.TipoFirmaId,
                        principalTable: "TipoFirmas",
                        principalColumn: "TipoFirmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuraciones_Nombre",
                table: "Configuraciones",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delitos_ExpedienteId",
                table: "Delitos",
                column: "ExpedienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_OrdenCapturaId",
                table: "Documentos",
                column: "OrdenCapturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_TipoDocumentoId",
                table: "Documentos",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmantes_Identificador",
                table: "Firmantes",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_OrdenCapturaId",
                table: "Firmas",
                column: "OrdenCapturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Firmas_TipoFirmaId",
                table: "Firmas",
                column: "TipoFirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesCaptura_OrdenCapturaEstadoId",
                table: "OrdenesCaptura",
                column: "OrdenCapturaEstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partes_ExpedienteId",
                table: "Partes",
                column: "ExpedienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuraciones");

            migrationBuilder.DropTable(
                name: "Delitos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Firmantes");

            migrationBuilder.DropTable(
                name: "Firmas");

            migrationBuilder.DropTable(
                name: "Partes");

            migrationBuilder.DropTable(
                name: "TipoDocumentos");

            migrationBuilder.DropTable(
                name: "OrdenesCaptura");

            migrationBuilder.DropTable(
                name: "TipoFirmas");

            migrationBuilder.DropTable(
                name: "Expedientes");

            migrationBuilder.DropTable(
                name: "OrdenCapturaEstados");
        }
    }
}
