using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Anexo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anexo1",
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
                    Ut_Gasoducto = table.Column<string>(type: "varchar(50)", maxLength:50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Proceso = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prestacion_Servicio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resultados_Analisis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resultados_Propuesta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo1", x => x.Id_PropuestaCambio);
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto2",
                table: "Anexo1",
                column: "Id_Proyecto"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Residencia1",
                table: "Anexo1",
                column: "Id_Residencia"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexo1");

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
