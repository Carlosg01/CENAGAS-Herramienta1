using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCenagas_v2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Idempleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Paterno = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Materno = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "set('Ingeniero','Ing.')", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    //Puesto = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                    //  .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "varchar(220)", maxLength: 220, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Idempleado);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Idproyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Folio_adc = table.Column<string>(type: "varchar(19)", maxLength: 19, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(220)", maxLength: 220, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InstalacionArea = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoCambio = table.Column<string>(type: "set('permanente','temporal','definitivo','nuevo rehabilitado','modificado')", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Idproyecto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Asignacion",
                columns: table => new
                {
                    Idasignacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Idempleado = table.Column<int>(type: "int", nullable: true),
                    Idproyecto = table.Column<int>(type: "int", nullable: true),
                    Idresidencia = table.Column<int>(type: "int", nullable: false),
                    Fechaalta = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fechabaja = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Funcion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignacion", x => x.Idasignacion);
                    table.ForeignKey(
                        name: "FK_Empleado",
                        column: x => x.Idempleado,
                        principalTable: "Empleado",
                        principalColumn: "Idempleado",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Proyecto",
                        column: x => x.Idproyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Idproyecto",
                        onDelete: ReferentialAction.SetNull
                        );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            /*Llaves foraneas de la tabla Asignacion*/
            migrationBuilder.CreateIndex(
                name: "Index_Empleado",
                table: "Asignacion",
                column: "Idempleado"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto",
                table: "Asignacion",
                column: "Idproyecto"
                );

            migrationBuilder.CreateTable(
                name: "DetalleProyecto",
                columns: table => new
                {
                    IdDetalleProyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProyecto = table.Column<int>(type: "int", nullable: false),
                    IdResidencia = table.Column<int>(type: "int", nullable: false),
                    IdAsignacion = table.Column<int>(type: "int", nullable: true),
                    NoDesarrollo = table.Column<int>(type: "int", nullable: false),
                    Desarrollo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionActividad = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avance = table.Column<int>(type: "int", nullable: false),
                    FaltanteComentarios = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comentarios = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlanAccion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anexos = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoProyecto = table.Column<string>(type: "set('ADC','Prearranque')", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleProyecto", x => x.IdDetalleProyecto);
                    table.ForeignKey(
                        name: "FK_Asignacion",
                        column: x => x.IdAsignacion,
                        principalTable: "Asignacion",
                        principalColumn: "Idasignacion",
                        onDelete: ReferentialAction.Restrict
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            /*Llaves foraneas de la tabla DetalleProyecto*/
            migrationBuilder.CreateIndex(
                name: "Index_Asignacion",
                table: "DetalleProyecto",
                column: "IdAsignacion"
                );

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignacion");

            migrationBuilder.DropTable(
                name: "DetalleProyecto");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Proyectos");
        }
    }
}
