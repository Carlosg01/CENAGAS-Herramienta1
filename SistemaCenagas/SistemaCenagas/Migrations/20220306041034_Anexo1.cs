using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Anexo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anexo1_PropuestaCambio",
                columns: table => new
                {
                    Id_PropuestaCambio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Tipo_Cambio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Residencia = table.Column<int>(type: "int", nullable: true),
                    Sector_Area = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Planta_Instalacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Proceso = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prestacion_Servicio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resultados_Analisis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_ProponenteCambio = table.Column<int>(type: "int", nullable: true),
                    Id_ResponsableADC = table.Column<int>(type: "int", nullable: true),
                    Resultados_Propuesta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo1_PropuestaCambio", x => x.Id_PropuestaCambio);
                    table.ForeignKey(
                        name: "FK_Proyecto2",
                        column: x => x.Id_Proyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id_Proyecto",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Residencia1",
                        column: x => x.Id_Residencia,
                        principalTable: "Residencias",
                        principalColumn: "Id_Residencia",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_ProponenteCambio",
                        column: x => x.Id_ProponenteCambio,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_ResponsableADC",
                        column: x => x.Id_ResponsableADC,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto2",
                table: "Anexo1_PropuestaCambio",
                column: "Id_Proyecto"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Residencia1",
                table: "Anexo1_PropuestaCambio",
                column: "Id_Residencia"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ProponenteCambio",
                table: "Anexo1_PropuestaCambio",
                column: "Id_ProponenteCambio"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ResponsableADC",
                table: "Anexo1_PropuestaCambio",
                column: "Id_ResponsableADC"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexo1_PropuestaCambio");

            migrationBuilder.AlterColumn<string>(
                name: "Id_Anexo",
                table: "ADC_Normativas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
