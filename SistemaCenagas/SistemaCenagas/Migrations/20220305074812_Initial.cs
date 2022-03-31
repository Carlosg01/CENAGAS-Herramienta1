using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Privilegios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmar_Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nueva_Password = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Rol = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Paterno = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Materno = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "varchar(220)", maxLength: 220, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image_Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuario);
                    table.ForeignKey(
                        name: "FK_Rol",
                        column: x => x.Id_Rol,
                        principalTable: "Roles",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Rol",
                table: "Usuarios",
                column: "Id_Rol"
                );

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id_Proyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),                    
                    Estado_ADC = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id_Proyecto);                    
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Proyecto_Miembros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto_Miembros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyecto_ProyectoMiembros",
                        column: x => x.Id_Proyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id_Proyecto",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Usuario_ProyectoMiembros",
                        column: x => x.Id_Usuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto_ProyectoMiembros",
                table: "Proyecto_Miembros",
                column: "Id_Proyecto"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Usuario_ProyectoMiembros",
                table: "Proyecto_Miembros",
                column: "Id_Usuario"
                );


            migrationBuilder.CreateTable(
                name: "Residencias",
                columns: table => new
                {
                    Id_Residencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residencias", x => x.Id_Residencia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADC",
                columns: table => new
                {
                    Id_ADC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Folio = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Id_ProponenteCambio = table.Column<int>(type: "int", nullable: true),
                    Id_Lider = table.Column<int>(type: "int", nullable: true),
                    Id_ResponsableADC = table.Column<int>(type: "int", nullable: true),
                    Id_Suplente = table.Column<int>(type: "int", nullable: true),
                    Fecha_Actualizacion = table.Column<string>(type: "longtext", nullable: true),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC", x => x.Id_ADC);
                    table.ForeignKey(
                        name: "FK_Proyecto1",
                        column: x => x.Id_Proyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id_Proyecto",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_ProponenteCambio1",
                        column: x => x.Id_ProponenteCambio,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Lider1",
                        column: x => x.Id_Lider,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_ResponsableADC1",
                        column: x => x.Id_ResponsableADC,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Suplente1",
                        column: x => x.Id_Suplente,
                        principalTable: "Usuarios",
                        principalColumn: "Id_Usuario",
                        onDelete: ReferentialAction.SetNull
                    );
                    
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto1",
                table: "ADC",
                column: "Id_Proyecto"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ProponenteCambio",
                table: "ADC",
                column: "Id_ProponenteCambio"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Lider1",
                table: "ADC",
                column: "Id_Lider"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ResponsableADC1",
                table: "ADC",
                column: "Id_ResponsableADC"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Suplente1",
                table: "ADC",
                column: "Id_Suplente"
                );
            

            migrationBuilder.CreateTable(
                name: "ADC_Actividades",
                columns: table => new
                {
                    Id_Actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Actividad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Actividades", x => x.Id_Actividad);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADC_Archivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<float>(type: "float", nullable: true),
                    Ubicacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Archivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADC_Archivos_IdADC",
                        column: x => x.Id_ADC,
                        principalTable: "ADC",
                        principalColumn: "Id_ADC",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_ADC_Archivos_IdADC",
                table: "ADC_Archivos",
                column: "Id_ADC"
                );

            migrationBuilder.CreateTable(
                name: "ADC_Procesos",
                columns: table => new
                {
                    Id_Proceso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Avance = table.Column<float>(type: "float", nullable: true),
                    Faltante_Comentarios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Plan_Accion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Procesos", x => x.Id_Proceso);
                    table.ForeignKey(
                        name: "FK_Actividad1",
                        column: x => x.Id_Actividad,
                        principalTable: "ADC_Actividades",
                        principalColumn: "Id_Actividad",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_ADC1",
                        column: x => x.Id_ADC,
                        principalTable: "ADC",
                        principalColumn: "Id_ADC",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Actividad1",
                table: "ADC_Procesos",
                column: "Id_Actividad"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ADC1",
                table: "ADC_Procesos",
                column: "Id_ADC"
                );

            migrationBuilder.CreateTable(
                name: "Gasoductos",
                columns: table => new
                {
                    Id_Instalacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gasoducto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Ducto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diametro_o_pulgadas = table.Column<float>(type: "float", nullable: true),
                    Denominacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Ut_Pemex = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Pemex = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasoductos", x => x.Id_Instalacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tramos",
                columns: table => new
                {
                    Id_Tramo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Tramo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tramo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km_Inicio = table.Column<float>(type: "float", nullable: true),
                    Km_Fin = table.Column<float>(type: "float", nullable: true),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Diametro = table.Column<float>(type: "float", nullable: true),
                    Espesor_Nominal = table.Column<float>(type: "float", nullable: true),
                    SMYS = table.Column<float>(type: "float", nullable: true),
                    Fecha_Construccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Residencia = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramos", x => x.Id_Tramo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                columns: table => new
                {
                    Id_Instalacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Instalacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Instalacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clase = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km = table.Column<float>(type: "float", maxLength: 200, nullable: true),
                    Residencia = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_X_decimal = table.Column<float>(type: "float", nullable: true),
                    Latitud_Y_decimal = table.Column<float>(type: "float", nullable: true),
                    Altitud_Z_decimal = table.Column<float>(type: "float", nullable: true),
                    Sector_Pemex = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gmas_Pemex = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalaciones", x => x.Id_Instalacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADC");

            migrationBuilder.DropTable(
                name: "ADC_Actividades");

            migrationBuilder.DropTable(
                name: "ADC_Procesos");

            migrationBuilder.DropTable(
                name: "Gasoductos");

            migrationBuilder.DropTable(
                name: "Instalaciones");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Residencias");

            migrationBuilder.DropTable(
                name: "Tramos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Proyecto_Miembros");
        }
    }
}
