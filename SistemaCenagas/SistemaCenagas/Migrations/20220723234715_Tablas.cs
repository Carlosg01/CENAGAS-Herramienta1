using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Tablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            #region Roles

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Privilegios = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Puestos

            migrationBuilder.CreateTable(
                name: "Puestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Privilegios = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puestos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Usuarios

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Token = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Rol = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmar_Password = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nueva_Password = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Puesto = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Paterno = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Materno = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image_Url = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_Proyecto = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_Tarea = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_ADC = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rol_Usuarios",
                        column: x => x.Id_Rol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                        name: "FK_Puesto_Usuarios",
                        column: x => x.Id_Puesto,
                        principalTable: "Puestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Rol_Usuarios",
                table: "Usuarios",
                column: "Id_Rol"
            );
            migrationBuilder.CreateIndex(
                name: "Index_Puesto_Usuarios",
                table: "Usuarios",
                column: "Id_Puesto"
            );

            #endregion

            #region DDV

            migrationBuilder.CreateTable(
                name: "DDV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDV", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Unidad

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abreviatura = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Direccion_Ejecutiva

            migrationBuilder.CreateTable(
                name: "Direccion_Ejecutiva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abreviatura = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Unidad = table.Column<int>(type: "int", nullable: true),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion_Ejecutiva", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Direccion_Ejecutiva__IdUnidad",
                       column: x => x.Id_Unidad,
                       principalTable: "Unidad",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Direccion_Ejecutiva__IdUnidad",
                table: "Direccion_Ejecutiva",
                column: "Id_Unidad"
                );

            #endregion

            #region Direccion

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abreviatura = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_DireccionEjecutiva = table.Column<int>(type: "int", nullable: true),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Direccion__IdDireccionEjecutiva",
                       column: x => x.Id_DireccionEjecutiva,
                       principalTable: "Direccion_Ejecutiva",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Direccion__IdDireccionEjecutiva",
                table: "Direccion",
                column: "Id_DireccionEjecutiva"
                );
            #endregion

            #region ElementoS3S

            migrationBuilder.CreateTable(
                name: "ElementoS3S",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Elemento = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementoS3S", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Especialidades

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Estados

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Pais = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capital = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<double>(type: "double", nullable: false),
                    Longitud = table.Column<double>(type: "double", nullable: false),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Etapa_Realizada

            migrationBuilder.CreateTable(
                name: "Etapa_Realizada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Etapa = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avance = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapa_Realizada", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Fuente_Direccion

            migrationBuilder.CreateTable(
                name: "Fuente_Deteccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fuente = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Abreviatura = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuente_Deteccion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Residencias

            migrationBuilder.CreateTable(
                name: "Residencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Estado = table.Column<int>(type: "int", nullable: true),
                    Codigo_Postal = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residencias", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Residencias__IdEstado",
                       column: x => x.Id_Estado,
                       principalTable: "Estados",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Residencias__IdEstado",
                table: "Residencias",
                column: "Id_Estado"
                );

            #endregion

            #region Sistema

            migrationBuilder.CreateTable(
                name: "Sistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Denominacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistema", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Tipo

            migrationBuilder.CreateTable(
                name: "Tipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abreviatura = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_Instalacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resumen = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Zonas

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion


            #region Proyectos

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado_ADC = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado_PreArranque = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Proyecto Miembros

            migrationBuilder.CreateTable(
                name: "Proyecto_Miembros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto_Miembros", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Proyecto_ProyectoMiembros",
                       column: x => x.Id_Proyecto,
                       principalTable: "Proyectos",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                       name: "FK_Usuario_ProyectoMiembros",
                       column: x => x.Id_Usuario,
                       principalTable: "Usuarios",
                       principalColumn: "Id",
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

            #endregion

            #region ADC_Anexos

            migrationBuilder.CreateTable(
                name: "ADC_Anexos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Gasoductos

            migrationBuilder.CreateTable(
                name: "Gasoductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gasoducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Ducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diametro_o_pulgadas = table.Column<float>(type: "float", nullable: true),
                    Denominacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Ut_Pemex = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Pemex = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasoductos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Instalaciones

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Instalacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Instalacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clase = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km = table.Column<float>(type: "float", nullable: true),
                    Residencia = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_X_decimal = table.Column<float>(type: "float", nullable: true),
                    Latitud_Y_decimal = table.Column<float>(type: "float", nullable: true),
                    Altitud_Z_decimal = table.Column<float>(type: "float", nullable: true),
                    Sector_Pemex = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gmas_Pemex = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Tramos

            migrationBuilder.CreateTable(
                name: "Tramos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Tramo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tramo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km_Inicio = table.Column<float>(type: "float", nullable: true),
                    Km_Fin = table.Column<float>(type: "float", nullable: true),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Diametro = table.Column<float>(type: "float", nullable: true),
                    Espesor_Nominal = table.Column<float>(type: "float", nullable: true),
                    SMYS = table.Column<float>(type: "float", nullable: true),
                    Fecha_Construccion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Residencia = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region ADC

            migrationBuilder.CreateTable(
                name: "ADC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Folio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_ProponenteCambio = table.Column<int>(type: "int", nullable: true),
                    Id_ResponsableADC = table.Column<int>(type: "int", nullable: true),
                    Id_Suplente = table.Column<int>(type: "int", nullable: true),
                    Id_LiderEquipoVerificador = table.Column<int>(type: "int", nullable: true),
                    Fecha_Actualizacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PreArranque = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Proyecto_ADC",
                      column: x => x.Id_Proyecto,
                      principalTable: "Proyectos",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Proponente_ADC",
                      column: x => x.Id_ProponenteCambio,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Lider_ADC",
                      column: x => x.Id_LiderEquipoVerificador,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Responsable_ADC",
                      column: x => x.Id_ResponsableADC,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Suplente_ADC",
                      column: x => x.Id_Suplente,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto_ADC",
                table: "ADC",
                column: "Id_Proyecto"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Proponente_ADC",
                table: "ADC",
                column: "Id_ProponenteCambio"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Lider_ADC",
                table: "ADC",
                column: "Id_LiderEquipoVerificador"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Responsable_ADC",
                table: "ADC",
                column: "Id_ResponsableADC"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Suplente_ADC",
                table: "ADC",
                column: "Id_Suplente"
                );

            #endregion

            #region ADC_Actividades

            migrationBuilder.CreateTable(
                name: "ADC_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Actividades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region ADC_Procesos

            migrationBuilder.CreateTable(
                name: "ADC_Procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Avance = table.Column<float>(type: "float", nullable: true),
                    Faltante_Comentarios = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Plan_Accion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Terminado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Procesos", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Actividad_ADCProcesos",
                      column: x => x.Id_Actividad,
                      principalTable: "ADC_Actividades",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_ADC_ADCProcesos",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Actividad_ADCProcesos",
                table: "ADC_Procesos",
                column: "Id_Actividad"
                );
            migrationBuilder.CreateIndex(
                name: "Index_ADC_ADCProcesos",
                table: "ADC_Procesos",
                column: "Id_ADC"
                );

            #endregion

            #region ADC_Archivos

            migrationBuilder.CreateTable(
                name: "ADC_Archivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Proceso = table.Column<int>(type: "int", nullable: true),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<float>(type: "float", nullable: true),
                    Ubicacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Archivos", x => x.Id);
                    table.ForeignKey(
                      name: "FK_ADC_ADCArchivos",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Proceso__ADC_Archivos",
                      column: x => x.Id_Proceso,
                      principalTable: "ADC_Procesos",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Usuario_ADCArchivos",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_ADC_ADCArchivos",
               table: "ADC_Archivos",
               column: "Id_ADC"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Proceso__ADC_Procesos",
               table: "ADC_Archivos",
               column: "Id_Proceso"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Usuario_ADCArchivos",
               table: "ADC_Archivos",
               column: "Id_Usuario"
               );
            #endregion

            #region ADC Normativas

            migrationBuilder.CreateTable(
                name: "ADC_Normativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Normativas", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Actividad_ADCNormativas",
                      column: x => x.Id_Actividad,
                      principalTable: "ADC_Actividades",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Actividad_ADCNormativas",
               table: "ADC_Normativas",
               column: "Id_Actividad"
               );
            #endregion

            #region ADC_Equipo_Verificador

            migrationBuilder.CreateTable(
                name: "ADC_Equipo_Verificador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_LiderEquipoVerificador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Equipo_Verificador", x => x.Id);
                    table.ForeignKey(
                      name: "FK_ADC_Equipo_Verificador",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Lider_Equipo_Verificador",
                      column: x => x.Id_LiderEquipoVerificador,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_ADC_Equipo_Verificador",
               table: "ADC_Equipo_Verificador",
               column: "Id_ADC"
               );
            migrationBuilder.CreateIndex(
              name: "Index_Lider_Equipo_Verificador",
              table: "ADC_Equipo_Verificador",
              column: "Id_LiderEquipoVerificador"
              );

            #endregion

            #region ADC_Equipo_Verificador_Integrantes

            migrationBuilder.CreateTable(
                name: "ADC_Equipo_Verificador_Integrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Equipo_Verificador_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Equipo_Verificador_Integrantes", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Equipo_Verificador__ADC_Equipo_Verificador_Integrantes",
                      column: x => x.Id_Equipo_Verificador_ADC,
                      principalTable: "ADC_Equipo_Verificador",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Usuario_Equipo_Verificador_Integrantes",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Equipo_Verificador__ADC_Equipo_Verificador_Integrantes",
               table: "ADC_Equipo_Verificador_Integrantes",
               column: "Id_Equipo_Verificador_ADC"
               );
            migrationBuilder.CreateIndex(
              name: "Index_Usuario__ADC_Equipo_Verificador_Integrantes",
              table: "ADC_Equipo_Verificador_Integrantes",
              column: "Id_Usuario"
              );

            #endregion

            #region ADC_Anexo2

            migrationBuilder.CreateTable(
                name: "ADC_Anexo2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo2", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Proyecto__Anexo2",
                      column: x => x.Id_Proyecto,
                      principalTable: "Proyectos",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Proyecto__Anexo2",
               table: "ADC_Anexo2",
               column: "Id_Proyecto"
               );
            #endregion

            #region ADC_Anexo1

            migrationBuilder.CreateTable(
                name: "ADC_Anexo1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Tipo_Cambio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Residencia = table.Column<int>(type: "int", nullable: true),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Proceso = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prestacion_Servicio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resultados_Analisis = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Resultados_Propuesta = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true),

                    EstatusADC = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PresentoARP = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo1", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Proyecto_Anexo1",
                       column: x => x.Id_Proyecto,
                       principalTable: "Proyectos",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                       name: "FK_Residencia_Anexo1",
                       column: x => x.Id_Residencia,
                       principalTable: "Residencias",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                       name: "FK_Anexo2__Anexo1",
                       column: x => x.Id_Anexo2,
                       principalTable: "ADC_Anexo2",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
              name: "Index_Proyecto_Anexo1",
              table: "ADC_Anexo1",
              column: "Id_Proyecto"
              );
            migrationBuilder.CreateIndex(
              name: "Index_Residencia_Anexo1",
              table: "ADC_Anexo1",
              column: "Id_Residencia"
              );
            migrationBuilder.CreateIndex(
              name: "Index_Anexo2__Anexo1",
              table: "ADC_Anexo1",
              column: "Id_Anexo2"
              );
            #endregion

            #region ADC_Anexo3

            migrationBuilder.CreateTable(
                name: "ADC_Anexo3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha_Registro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_ADC = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),

                    Equipo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instrumento = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Componente_o_Dispositivo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valvula = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Accesorio_o_Ducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estacion_Compresion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estacion_Medicion_y_Regulacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Trampa_Envios_y_Recibo_Diablos = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Otro_Elemento = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Numero_Identificacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Riesgo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Inversion_Cambio = table.Column<double>(type: "double", nullable: true),
                    Fecha_Inicio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Termino = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Justificacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Cambio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Otra_Documentacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Id_Responsable_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Seguridad_Industrial = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Ejecutivo_Operacion = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Ejecutivo_Mantenimiento_y_Seguridad = table.Column<int>(type: "int", nullable: true),

                    Id_Anexo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo3__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo1",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Responsable_ADC",
                      column: x => x.Id_Responsable_ADC,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Seguridad_Industrial",
                      column: x => x.Id_Director_Seguridad_Industrial,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Ejecutivo_Operacion",
                      column: x => x.Id_Director_Ejecutivo_Operacion,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Ejecutivo_Mantenimiento_y_Seguridad",
                      column: x => x.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Anexo2",
                      column: x => x.Id_Anexo2,
                      principalTable: "ADC_Anexo2",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Anexo1",
               table: "ADC_Anexo3",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Responsable_ADC",
               table: "ADC_Anexo3",
               column: "Id_Responsable_ADC"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Seguridad_Industrial",
               table: "ADC_Anexo3",
               column: "Id_Director_Seguridad_Industrial"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Ejecutivo_Operacion",
               table: "ADC_Anexo3",
               column: "Id_Director_Ejecutivo_Operacion"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Ejecutivo_Mantenimiento_y_Seguridad",
               table: "ADC_Anexo3",
               column: "Id_Director_Ejecutivo_Mantenimiento_y_Seguridad"
               );
            migrationBuilder.CreateIndex(
               name: "FK_Anexo3__Anexo2",
               table: "ADC_Anexo3",
               column: "Id_Anexo2"
               );
            #endregion

            #region ADC_Anexo4

            migrationBuilder.CreateTable(
                name: "ADC_Anexo4",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Fecha_Retiro_Cambio_Temporal = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tiempo_Estimado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Propuesta_Inicio_Operacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Autorizacion_Inicio_Operacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Id_Residente = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo4__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo1",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Anexo3",
                      column: x => x.Id_Anexo3,
                      principalTable: "ADC_Anexo3",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Residente",
                      column: x => x.Id_Residente,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Anexo1",
               table: "ADC_Anexo4",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Anexo3",
               table: "ADC_Anexo4",
               column: "Id_Anexo3"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Residente",
               table: "ADC_Anexo4",
               column: "Id_Residente"
               );
            #endregion

            #region ADC_Anexo5

            migrationBuilder.CreateTable(
                name: "ADC_Anexo5",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Responsable_Cambio_Temporal = table.Column<int>(type: "int", nullable: true),

                    Dia_Cambio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mes_Cambio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio_Cambio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dia_Retiro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mes_Retiro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio_Retiro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmacion_Retiro_Cambios_Temporales = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo5__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo1",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Id_Responsable_Cambio_Temporal",
                      column: x => x.Id_Responsable_Cambio_Temporal,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo5__Anexo3",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo3",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Anexo1",
               table: "ADC_Anexo5",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Id_Responsable_Cambio_Temporal",
               table: "ADC_Anexo5",
               column: "Id_Responsable_Cambio_Temporal"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Anexo3",
               table: "ADC_Anexo5",
               column: "Id_Anexo3"
               );
            #endregion

            #region ADC_Anexo6

            migrationBuilder.CreateTable(
                name: "ADC_Anexo6",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha_Recepcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Fecha_Inicio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Termino = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo6__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo1",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo6__Anexo3",
                      column: x => x.Id_Anexo1,
                      principalTable: "ADC_Anexo3",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6__Anexo1",
               table: "ADC_Anexo6",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6__Anexo3",
               table: "ADC_Anexo6",
               column: "Id_Anexo3"
               );
            #endregion

            #region ADC_Anexo3_CatalogoTipoDocumentacion

            migrationBuilder.CreateTable(
                name: "ADC_Anexo3_CatalogoTipoDocumentacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoDocumentacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3_Documentacion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region ADC_Anexo3_Documentacion

            migrationBuilder.CreateTable(
                name: "ADC_Anexo3_Documentacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Id_Tipo = table.Column<int>(type: "int", nullable: true),
                    Check = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anotaciones = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable_Area = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3_Documentacion", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo3_Documentacion__Anexo3",
                      column: x => x.Id_Anexo3,
                      principalTable: "ADC_Anexo3",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3_Documentacion__Id_Tipo",
                      column: x => x.Id_Tipo,
                      principalTable: "ADC_Anexo3_CatalogoTipoDocumentacion",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_Documentacion__Anexo3",
               table: "ADC_Anexo3_Documentacion",
               column: "Id_Anexo3"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_Documentacion__Id_Tipo",
               table: "ADC_Anexo3_Documentacion",
               column: "Id_Tipo"
               );
            #endregion

            #region ADC_Anexo3_DocumentacionResponsable

            migrationBuilder.CreateTable(
                name: "ADC_Anexo3_DocumentacionResponsable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Responsable = table.Column<int>(type: "int", nullable: true),
                    Check = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Id_Documentacion = table.Column<int>(type: "int", nullable: true),

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3_DocumentacionResponsable", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo3_DocumentacionResponsable__IdResponsable",
                      column: x => x.Id_Responsable,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3_DocumentacionResponsable__IdAnexo3",
                      column: x => x.Id_Anexo3,
                      principalTable: "ADC_Anexo3",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3_Documentacion__IdDocumentacion",
                      column: x => x.Id_Documentacion,
                      principalTable: "ADC_Anexo3_Documentacion",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_DocumentacionResponsable__IdResponsable",
               table: "ADC_Anexo3_DocumentacionResponsable",
               column: "Id_Responsable"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_DocumentacionResponsable__IdAnexo3",
               table: "ADC_Anexo3_DocumentacionResponsable",
               column: "Id_Anexo3"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_DocumentacionResponsable__IdDocumentacion",
               table: "ADC_Anexo3_DocumentacionResponsable",
               column: "Id_Documentacion"
               );
            #endregion

            #region ADC_Anexo6_Documentacion_Catalogo

            migrationBuilder.CreateTable(
                name: "ADC_Anexo6_Documentacion_Catalogo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Elemento = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Seccion = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo6_Documentacion_Catalogo", x => x.Id);

                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region ADC_Anexo6_Documentacion

            migrationBuilder.CreateTable(
                name: "ADC_Anexo6_Documentacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo6 = table.Column<int>(type: "int", nullable: true),
                    Id_Elemento_Catalogo = table.Column<int>(type: "int", nullable: true),
                    Check = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo6_Documentacion", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Anexo6_Documentacion__Anexo6",
                      column: x => x.Id_Anexo6,
                      principalTable: "ADC_Anexo6",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo6_Documentacion__IdElemento",
                      column: x => x.Id_Elemento_Catalogo,
                      principalTable: "ADC_Anexo6_Documentacion_Catalogo",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6_Documentacion__Anexo6",
               table: "ADC_Anexo6_Documentacion",
               column: "Id_Anexo6"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6_Documentacion__IdElemento",
               table: "ADC_Anexo6_Documentacion",
               column: "Id_Elemento_Catalogo"
               );

            #endregion


            #region PreArranque

            migrationBuilder.CreateTable(
                name: "PreArranque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Con_ADC = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Folio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    //Id_ProponenteCambio = table.Column<int>(type: "int", nullable: true),
                    Id_Responsable = table.Column<int>(type: "int", nullable: true),
                    Id_Suplente = table.Column<int>(type: "int", nullable: true),
                    Id_LiderEquipoVerificador = table.Column<int>(type: "int", nullable: true),
                    Fecha_Actualizacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque", x => x.Id);
                    /*table.ForeignKey(
                      name: "FK_PreArranque__Id_ADC",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );*/
                    table.ForeignKey(
                      name: "FK_Proyecto_PreArranque",
                      column: x => x.Id_Proyecto,
                      principalTable: "Proyectos",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    /*table.ForeignKey(
                      name: "FK_Proponente_PreArranque",
                      column: x => x.Id_ProponenteCambio,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );*/
                    table.ForeignKey(
                      name: "FK_Lider_PreArranque",
                      column: x => x.Id_LiderEquipoVerificador,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Responsable_PreArranque",
                      column: x => x.Id_Responsable,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Suplente_PreArranque",
                      column: x => x.Id_Suplente,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            /*migrationBuilder.CreateIndex(
                name: "Index_PreArranque__Id_ADC",
                table: "PreArranque",
                column: "Id_ADC"
                );*/
            migrationBuilder.CreateIndex(
                name: "Index_Proyecto_PreArranque",
                table: "PreArranque",
                column: "Id_Proyecto"
                );
            /*migrationBuilder.CreateIndex(
                name: "Index_Proponente_PreArranque",
                table: "PreArranque",
                column: "Id_ProponenteCambio"
                );*/
            migrationBuilder.CreateIndex(
                name: "Index_Lider_PreArranque",
                table: "PreArranque",
                column: "Id_LiderEquipoVerificador"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Responsable_PreArranque",
                table: "PreArranque",
                column: "Id_Responsable"
                );
            migrationBuilder.CreateIndex(
                name: "Index_Suplente_PreArranque",
                table: "PreArranque",
                column: "Id_Suplente"
                );

            #endregion

            #region PreArranque_Actividades

            migrationBuilder.CreateTable(
                name: "PreArranque_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Actividades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region PreArranque_Procesos

            migrationBuilder.CreateTable(
                name: "PreArranque_Procesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Id_PreArranque = table.Column<int>(type: "int", nullable: true),
                    Avance = table.Column<float>(type: "float", nullable: true),
                    Faltante_Comentarios = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Plan_Accion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Terminado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmado = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Procesos", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Actividad_PreArranqueProcesos",
                      column: x => x.Id_Actividad,
                      principalTable: "PreArranque_Actividades",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_PreArranque_PreArranqueProcesos",
                      column: x => x.Id_PreArranque,
                      principalTable: "PreArranque",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Actividad_PreArranqueProcesos",
                table: "PreArranque_Procesos",
                column: "Id_Actividad"
                );
            migrationBuilder.CreateIndex(
                name: "Index_PreArranque_PreArranqueProcesos",
                table: "PreArranque_Procesos",
                column: "Id_PreArranque"
                );

            #endregion

            #region PreArranque_Archivos

            migrationBuilder.CreateTable(
                name: "PreArranque_Archivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_PreArranque = table.Column<int>(type: "int", nullable: true),
                    Id_Proceso = table.Column<int>(type: "int", nullable: true),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<float>(type: "float", nullable: true),
                    Ubicacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Archivos", x => x.Id);
                    table.ForeignKey(
                      name: "FK_PreArranque_PreArranqueArchivos",
                      column: x => x.Id_PreArranque,
                      principalTable: "PreArranque",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Proceso__PreArranque_Archivos",
                      column: x => x.Id_Proceso,
                      principalTable: "PreArranque_Procesos",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Usuario_PreArranqueArchivos",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_PreArranqueArchivos",
               table: "PreArranque_Archivos",
               column: "Id_PreArranque"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Proceso__PreArranque_Procesos",
               table: "PreArranque_Archivos",
               column: "Id_Proceso"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Usuario_PreArranqueArchivos",
               table: "PreArranque_Archivos",
               column: "Id_Usuario"
               );
            #endregion

            #region PreArranque Normativas

            migrationBuilder.CreateTable(
                name: "PreArranque_Normativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Normativas", x => x.Id);
                    table.ForeignKey(
                      name: "FK_Actividad_PreArranqueNormativas",
                      column: x => x.Id_Actividad,
                      principalTable: "PreArranque_Actividades",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Actividad_PreArranqueNormativas",
               table: "PreArranque_Normativas",
               column: "Id_Actividad"
               );
            #endregion

            #region PreArranque_Equipo_Verificador

            migrationBuilder.CreateTable(
                name: "PreArranque_Equipo_Verificador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_PreArranque = table.Column<int>(type: "int", nullable: true),
                    Id_LiderEquipoVerificador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Equipo_Verificador", x => x.Id);
                    table.ForeignKey(
                      name: "FK_PreArranque_Equipo_Verificador__Id_PreArranque",
                      column: x => x.Id_PreArranque,
                      principalTable: "PreArranque",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_PreArranque_Equipo_Verificador__LiderEquipoVerificador",
                      column: x => x.Id_LiderEquipoVerificador,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Equipo_Verificador__Id_PreArranque",
               table: "PreArranque_Equipo_Verificador",
               column: "Id_PreArranque"
               );
            migrationBuilder.CreateIndex(
              name: "Index_PreArranque_Equipo_Verificador__LiderEquipoVerificador",
              table: "PreArranque_Equipo_Verificador",
              column: "Id_LiderEquipoVerificador"
              );

            #endregion

            #region PreArranque_Equipo_Verificador_Integrantes

            migrationBuilder.CreateTable(
                name: "PreArranque_Equipo_Verificador_Integrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Equipo_Verificador_PreArranque = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Equipo_Verificador_Integrantes", x => x.Id);
                    table.ForeignKey(
                      name: "FK_PreArranque_Equipo_Verificador_Integrantes__Id_Equipo_Verificador_PreArranque",
                      column: x => x.Id_Equipo_Verificador_PreArranque,
                      principalTable: "PreArranque_Equipo_Verificador",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_PreArranque_Equipo_Verificador_Integrantes__Id_Usuario",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Equipo_Verificador_Integrantes__Id_Equipo_Verificador_PreArranque",
               table: "PreArranque_Equipo_Verificador_Integrantes",
               column: "Id_Equipo_Verificador_PreArranque"
               );
            migrationBuilder.CreateIndex(
              name: "Index_PreArranque_Equipo_Verificador_Integrantes__Id_Usuario",
              table: "PreArranque_Equipo_Verificador_Integrantes",
              column: "Id_Usuario"
              );

            #endregion

            #region PreArranque_Anexo2

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Numero_Revisiones = table.Column<int>(type: "int", nullable: true),
                    Revision_Actual = table.Column<int>(type: "int", nullable: true),
                    Fecha_Elaboracion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Residencia = table.Column<int>(type: "int", nullable: true),
                    Ut_Gasoducto = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Prearranque = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preArranque_Anexo2", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Residencia_PreArranque_Anexo2",
                       column: x => x.Id_Residencia,
                       principalTable: "Residencias",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                      name: "FK_IdPrearranque_PreArranque_Anexo2",
                      column: x => x.Id_Prearranque,
                      principalTable: "PreArranque",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
             name: "Index_Residencia_PreArranque_Anexo2",
             table: "PreArranque_Anexo2",
             column: "Id_Residencia"
             );
            migrationBuilder.CreateIndex(
             name: "Index_IdPrearranque_PreArranque_Anexo2",
             table: "PreArranque_Anexo2",
             column: "Id_Prearranque"
             );

            #endregion

            #region PreArranque_Anexo2_Seccion2

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2_Seccion2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo2_Seccion2", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Anexo2_PreArranque_Anexo2_Seccion2",
                       column: x => x.Id_Anexo2,
                       principalTable: "PreArranque_Anexo2",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
             name: "Index_Anexo2_PreArranque_Anexo2_Seccion2",
             table: "PreArranque_Anexo2_Seccion2",
             column: "Id_Anexo2"
             );

            #endregion

            #region PreArranque_Anexo2_Seccion2_ElementosRevision

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2_Seccion2_ElementosRevision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Elemento_Revision = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_Revision = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_Hallazgo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Atendido = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo2_Seccion2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo2_Seccion2_ElementosRevision", x => x.Id);
                    table.ForeignKey(
                       name: "FK_Anexo2Seccion2_PreArranque_Anexo2_Seccion2_ElementosRevision",
                       column: x => x.Id_Anexo2_Seccion2,
                       principalTable: "PreArranque_Anexo2_Seccion2",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
             name: "Index_Anexo2Seccion2_PreArranque_Anexo2_Seccion2_ElementosRevision",
             table: "PreArranque_Anexo2_Seccion2_ElementosRevision",
             column: "Id_Anexo2_Seccion2"
             );

            #endregion

            #region PreArranque_Anexo2_Seccion2_Catalogo

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2_Seccion2_Catalogo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tarea = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo2_Seccion2_Catalogo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Tarea = table.Column<int>(type: "int", nullable: true),
                    Subtarea = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo", x => x.Id);
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo__IdTarea",
                       column: x => x.Id_Tarea,
                       principalTable: "PreArranque_Anexo2_Seccion2_Catalogo",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
             name: "Index_PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo__IdTarea",
             table: "PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo",
             column: "Id_Tarea"
             );
            #endregion

            #region PreArranque_Anexo2_Seccion3

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo2_Seccion3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Hallazgo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Riesgo = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Recomendacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Responsable = table.Column<int>(type: "int", nullable: true),
                    Id_Anexo2_Seccion2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo2_Seccion3", x => x.Id);
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo2_Seccion3__Id_Responsable",
                       column: x => x.Id_Responsable,
                       principalTable: "Usuarios",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo2_Seccion3__Id_Anexo2Seccion2",
                       column: x => x.Id_Anexo2_Seccion2,
                       principalTable: "PreArranque_Anexo2_Seccion2_ElementosRevision",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
             name: "Index_PreArranque_Anexo2_Seccion3__Id_Responsable",
             table: "PreArranque_Anexo2_Seccion3",
             column: "Id_Responsable"
             );
            migrationBuilder.CreateIndex(
             name: "Index_PreArranque_Anexo2_Seccion3__Id_Anexo2Seccion2",
             table: "PreArranque_Anexo2_Seccion3",
             column: "Id_Anexo2_Seccion2"
             );

            #endregion

            #region PreArranque_Anexo1

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha_Elaboracion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Prearranque = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo1", x => x.Id);
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo1__Id_PreArranque",
                       column: x => x.Id_Prearranque,
                       principalTable: "PreArranque",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
             name: "Index_PreArranque_Anexo1__Id_PreArranque",
             table: "PreArranque_Anexo1",
             column: "Id_Prearranque"
             );

            #endregion

            #region PreArranque_Anexo1_Actividades

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo1_Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    //Id_Anexo2_Seccion2 = table.Column<int>(type: "int", nullable: true),
                    Accion_Descriptiva = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Responsable = table.Column<int>(type: "int", nullable: true),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo1_Actividades", x => x.Id);
                    /*
                    table.ForeignKey(
                        name: "FK_PreArranque_Anexo1_Actividades__Id_Anexo2_Seccion2",
                        column: x => x.Id_Anexo2_Seccion2,
                        principalTable: "PreArranque_Anexo2_Seccion2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );

                     table.ForeignKey(
                        name: "FK_PreArranque_Anexo1_Actividades__Id_Responsable",
                        column: x => x.Id_Responsable,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull
                    );
                     */
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo1_Actividades__Id_Anexo1",
                       column: x => x.Id_Anexo1,
                       principalTable: "PreArranque_Anexo1",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            /*
            migrationBuilder.CreateIndex(
                name: "Index_PreArranque_Anexo1_Actividades__Id_Anexo2_Seccion2",
                table: "PreArranque_Anexo1_Actividades",
                column: "Id_Anexo2_Seccion2"
            );
           
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Anexo1_Actividades__Id_Responsable",
               table: "PreArranque_Anexo1_Actividades",
               column: "Id_Responsable"
           );
            */
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Anexo1_Actividades__Id_Anexo1",
               table: "PreArranque_Anexo1_Actividades",
               column: "Id_Anexo1"
           );

            #endregion

            #region PreArranque_Anexo1_Actividades_Acciones

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo1_Actividades_Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo1_Actividades = table.Column<int>(type: "int", nullable: true),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Inicio = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Termino = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Evidencia = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avance = table.Column<float>(type: "float", nullable: false),
                    Responsable = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Concluida = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo1_Actividades_Acciones", x => x.Id);
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo1_Actividades_Acciones__Id_Anexo1_Actividades",
                       column: x => x.Id_Anexo1_Actividades,
                       principalTable: "PreArranque_Anexo1_Actividades",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Anexo1_Actividades_Acciones__Id_Anexo1_Actividades",
               table: "PreArranque_Anexo1_Actividades_Acciones",
               column: "Id_Anexo1_Actividades"
            );

            #endregion

            migrationBuilder.CreateTable(
                name: "PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Accion = table.Column<int>(type: "int", nullable: true),
                    Actividad = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<float>(type: "float", nullable: false),
                    Ubicacion = table.Column<string>(type: "varchar(900)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Eliminado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia", x => x.Id);
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia____IdAccion",
                       column: x => x.Id_Accion,
                       principalTable: "PreArranque_Anexo1_Actividades_Acciones",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                       name: "FK_PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia__IdUsuario",
                       column: x => x.Id_Usuario,
                       principalTable: "Usuarios",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index___PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia____IdAccion",
               table: "PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia",
               column: "Id_Accion"
            );
            migrationBuilder.CreateIndex(
               name: "Index_PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia__IdUsuario",
               table: "PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia",
               column: "Id_Usuario"
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADC");

            migrationBuilder.DropTable(
                name: "ADC_Actividades");

            migrationBuilder.DropTable(
                name: "ADC_Anexo1");

            migrationBuilder.DropTable(
                name: "ADC_Anexo2");

            migrationBuilder.DropTable(
                name: "ADC_Anexo3");

            migrationBuilder.DropTable(
                name: "ADC_Anexo3_CatalogoTipoDocumentacion");

            migrationBuilder.DropTable(
                name: "ADC_Anexo3_Documentacion");

            migrationBuilder.DropTable(
                name: "ADC_Anexo3_DocumentacionResponsable");

            migrationBuilder.DropTable(
                name: "ADC_Anexo4");

            migrationBuilder.DropTable(
                name: "ADC_Anexo5");

            migrationBuilder.DropTable(
                name: "ADC_Anexo6");

            migrationBuilder.DropTable(
                name: "ADC_Anexo6_Documentacion_Catalogo");

            migrationBuilder.DropTable(
                name: "ADC_Anexo6_Documentacion");

            migrationBuilder.DropTable(
                name: "ADC_Anexos");

            migrationBuilder.DropTable(
                name: "ADC_Archivos");

            migrationBuilder.DropTable(
                name: "ADC_Equipo_Verificador");

            migrationBuilder.DropTable(
                name: "ADC_Equipo_Verificador_Integrantes");

            migrationBuilder.DropTable(
                name: "ADC_Normativas");

            migrationBuilder.DropTable(
                name: "ADC_Procesos");

            migrationBuilder.DropTable(
                name: "DDV");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Direccion_Ejecitiva");

            migrationBuilder.DropTable(
                name: "ElementoS3S");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Etapa_Realizada");

            migrationBuilder.DropTable(
                name: "Fuente_Deteccion");

            migrationBuilder.DropTable(
                name: "Gasoductos");

            migrationBuilder.DropTable(
                name: "Instalaciones");

            migrationBuilder.DropTable(
                name: "PreArranque");

            migrationBuilder.DropTable(
                name: "PreArranque_Actividades");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo1");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo1_Actividades");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo1_Actividades_Acciones");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo1_Actividades_Acciones_ArchivosEvidencia");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2_Seccion2");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2_Seccion2_ElementosRevision");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2_Seccion2_Catalogo");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2_Seccion2_ElementosRevision_Catalogo");

            migrationBuilder.DropTable(
                name: "PreArranque_Anexo2_Seccion3");

            migrationBuilder.DropTable(
                name: "PreArranque_Archivos");

            migrationBuilder.DropTable(
                name: "PreArranque_Equipo_Verificador");

            migrationBuilder.DropTable(
                name: "PreArranque_Equipo_Verificador_Integrantes");

            migrationBuilder.DropTable(
                name: "PreArranque_Normativas");

            migrationBuilder.DropTable(
                name: "PreArranque_Procesos");

            migrationBuilder.DropTable(
                name: "Proyecto_Miembros");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Puestos");

            migrationBuilder.DropTable(
                name: "Residencias");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Sistema");

            migrationBuilder.DropTable(
                name: "Tipo");

            migrationBuilder.DropTable(
                name: "Tramos");

            migrationBuilder.DropTable(
                name: "Unidad");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Zonas");
        }
    }
}