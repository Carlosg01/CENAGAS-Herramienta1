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
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Privilegios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Usuarios

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Rol = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Confirmar_Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nueva_Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Puesto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Paterno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Materno = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image_Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_Proyecto = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_Tarea = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notificacion_ADC = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuario);
                    table.ForeignKey(
                        name: "FK_Rol_Usuarios",
                        column: x => x.Id_Rol,
                        principalTable: "Roles",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Rol_Usuarios",
                table: "Usuarios",
                column: "Id_Rol"
            );

            #endregion

            #region Residencias

            migrationBuilder.CreateTable(
                name: "Residencias",
                columns: table => new
                {
                    Id_Residencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residencias", x => x.Id_Residencia);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region Proyectos

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id_Proyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado_ADC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id_Proyecto);
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

            #endregion

            #region Anexos

            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    Id_Anexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id_Anexo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Gasoductos

            migrationBuilder.CreateTable(
                name: "Gasoductos",
                columns: table => new
                {
                    Id_Gasoducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Gasoducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gasoducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Ducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diametro_o_pulgadas = table.Column<float>(type: "float", nullable: true),
                    Denominacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Ut_Pemex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Pemex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gasoductos", x => x.Id_Gasoducto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Instalaciones

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                columns: table => new
                {
                    Id_Instalacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Instalacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Instalacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Clase = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km = table.Column<float>(type: "float", nullable: true),
                    Residencia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Region = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sistema = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud_X_decimal = table.Column<float>(type: "float", nullable: true),
                    Latitud_Y_decimal = table.Column<float>(type: "float", nullable: true),
                    Altitud_Z_decimal = table.Column<float>(type: "float", nullable: true),
                    Sector_Pemex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gmas_Pemex = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalaciones", x => x.Id_Instalacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region Tramos

            migrationBuilder.CreateTable(
                name: "Tramos",
                columns: table => new
                {
                    Id_Tramo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ut_Tramo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tramo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km_Inicio = table.Column<float>(type: "float", nullable: true),
                    Km_Fin = table.Column<float>(type: "float", nullable: true),
                    Longitud_Metros = table.Column<float>(type: "float", nullable: true),
                    Diametro = table.Column<float>(type: "float", nullable: true),
                    Espesor_Nominal = table.Column<float>(type: "float", nullable: true),
                    SMYS = table.Column<float>(type: "float", nullable: true),
                    Fecha_Construccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Residencia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Gasoducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramos", x => x.Id_Tramo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            #endregion

            #region ADC

            migrationBuilder.CreateTable(
                name: "ADC",
                columns: table => new
                {
                    Id_ADC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true),
                    Folio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_ProponenteCambio = table.Column<int>(type: "int", nullable: true),
                    Id_Lider = table.Column<int>(type: "int", nullable: true),
                    Id_ResponsableADC = table.Column<int>(type: "int", nullable: true),
                    Id_Suplente = table.Column<int>(type: "int", nullable: true),
                    Fecha_Actualizacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC", x => x.Id_ADC);
                    table.ForeignKey(
                      name: "FK_Proyecto_ADC",
                      column: x => x.Id_Proyecto,
                      principalTable: "Proyectos",
                      principalColumn: "Id_Proyecto",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Proponente_ADC",
                      column: x => x.Id_ProponenteCambio,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Lider_ADC",
                      column: x => x.Id_Lider,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Responsable_ADC",
                      column: x => x.Id_ResponsableADC,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Suplente_ADC",
                      column: x => x.Id_Suplente,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
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
                column: "Id_Lider"
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
                    Id_Actividad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Actividad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Actividades", x => x.Id_Actividad);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            #endregion

            #region ADC_Procesos

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
                      name: "FK_Actividad_ADCProcesos",
                      column: x => x.Id_Actividad,
                      principalTable: "ADC_Actividades",
                      principalColumn: "Id_Actividad",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_ADC_ADCProcesos",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id_ADC",
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
                    Actividad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
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
                      name: "FK_ADC_ADCArchivos",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id_ADC",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Proceso__ADC_Archivos",
                      column: x => x.Id_Proceso,
                      principalTable: "ADC_Procesos",
                      principalColumn: "Id_Proceso",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Usuario_ADCArchivos",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
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
                    Id_Normativa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo = table.Column<int>(type: "int", nullable: true),
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Normativas", x => x.Id_Normativa);
                    table.ForeignKey(
                      name: "FK_Actividad_ADCNormativas",
                      column: x => x.Id_Actividad,
                      principalTable: "ADC_Actividades",
                      principalColumn: "Id_Actividad",
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
                    Id_Equipo_Verificador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Lider = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Equipo_Verificador", x => x.Id_Equipo_Verificador);
                    table.ForeignKey(
                      name: "FK_ADC_Equipo_Verificador",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id_ADC",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Lider_Equipo_Verificador",
                      column: x => x.Id_Lider,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
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
              column: "Id_Lider"
              );

            #endregion

            #region ADC_Equipo_Verificador_Integrantes

            migrationBuilder.CreateTable(
                name: "ADC_Equipo_Verificador_Integrantes",
                columns: table => new
                {
                    Id_Equipo_Verificador_Integrantes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Equipo_Verificador = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Equipo_Verificador_Integrantes", x => x.Id_Equipo_Verificador_Integrantes);
                    table.ForeignKey(
                      name: "FK_Equipo_Verificador__ADC_Equipo_Verificador_Integrantes",
                      column: x => x.Id_Equipo_Verificador,
                      principalTable: "ADC_Equipo_Verificador",
                      principalColumn: "Id_Equipo_Verificador",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Usuario_Equipo_Verificador_Integrantes",
                      column: x => x.Id_Usuario,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Equipo_Verificador__ADC_Equipo_Verificador_Integrantes",
               table: "ADC_Equipo_Verificador_Integrantes",
               column: "Id_Equipo_Verificador"
               );
            migrationBuilder.CreateIndex(
              name: "Index_Usuario__ADC_Equipo_Verificador_Integrantes",
              table: "ADC_Equipo_Verificador_Integrantes",
              column: "Id_Usuario"
              );

            #endregion


            #region Anexo2

            migrationBuilder.CreateTable(
                name: "Anexo2",
                columns: table => new
                {
                    Id_Anexo2 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Proyecto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo2", x => x.Id_Anexo2);
                    table.ForeignKey(
                      name: "FK_Proyecto__Anexo2",
                      column: x => x.Id_Proyecto,
                      principalTable: "Proyectos",
                      principalColumn: "Id_Proyecto",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Proyecto__Anexo2",
               table: "Anexo2",
               column: "Id_Proyecto"
               );
            #endregion

            #region Anexo1

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
                    Ut_Gasoducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ut_Tramo = table.Column<string>(type: "longtext", nullable: true)
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
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true),

                    EstatusADC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PresentoARP = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo1", x => x.Id_PropuestaCambio);
                    table.ForeignKey(
                       name: "FK_Proyecto_Anexo1",
                       column: x => x.Id_Proyecto,
                       principalTable: "Proyectos",
                       principalColumn: "Id_Proyecto",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                       name: "FK_Residencia_Anexo1",
                       column: x => x.Id_Residencia,
                       principalTable: "Residencias",
                       principalColumn: "Id_Residencia",
                       onDelete: ReferentialAction.SetNull
                   );
                    table.ForeignKey(
                       name: "FK_Anexo2__Anexo1",
                       column: x => x.Id_Anexo2,
                       principalTable: "Anexo2",
                       principalColumn: "Id_Anexo2",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
              name: "Index_Proyecto_Anexo1",
              table: "Anexo1",
              column: "Id_Proyecto"
              );
            migrationBuilder.CreateIndex(
              name: "Index_Residencia_Anexo1",
              table: "Anexo1",
              column: "Id_Residencia"
              );
            migrationBuilder.CreateIndex(
              name: "Index_Anexo2__Anexo1",
              table: "Anexo1",
              column: "Id_Anexo2"
              );
            #endregion

            #region Anexo3

            migrationBuilder.CreateTable(
                name: "Anexo3",
                columns: table => new
                {
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha_Registro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_ADC = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),

                    Equipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instrumento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Componente_o_Dispositivo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valvula = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Accesorio_o_Ducto = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estacion_Compresion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estacion_Medicion_y_Regulacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Trampa_Envios_y_Recibo_Diablos = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Otro_Elemento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Numero_Identificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Riesgo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Inversion_Cambio = table.Column<double>(type: "double", nullable: true),
                    Fecha_Inicio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Termino = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Justificacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion_Cambio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Otra_Documentacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Id_Responsable_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Seguridad_Industrial = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Ejecutivo_Operacion = table.Column<int>(type: "int", nullable: true),
                    Id_Director_Ejecutivo_Mantenimiento_y_Seguridad = table.Column<int>(type: "int", nullable: true),

                    Id_Anexo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3", x => x.Id_Anexo3);
                    table.ForeignKey(
                      name: "FK_Anexo3__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo1",
                      principalColumn: "Id_PropuestaCambio",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Responsable_ADC",
                      column: x => x.Id_Responsable_ADC,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Seguridad_Industrial",
                      column: x => x.Id_Director_Seguridad_Industrial,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Ejecutivo_Operacion",
                      column: x => x.Id_Director_Ejecutivo_Operacion,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Id_Director_Ejecutivo_Mantenimiento_y_Seguridad",
                      column: x => x.Id_Director_Ejecutivo_Mantenimiento_y_Seguridad,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3__Anexo2",
                      column: x => x.Id_Anexo2,
                      principalTable: "Anexo2",
                      principalColumn: "Id_Anexo2",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Anexo1",
               table: "Anexo3",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Responsable_ADC",
               table: "Anexo3",
               column: "Id_Responsable_ADC"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Seguridad_Industrial",
               table: "Anexo3",
               column: "Id_Director_Seguridad_Industrial"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Ejecutivo_Operacion",
               table: "Anexo3",
               column: "Id_Director_Ejecutivo_Operacion"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3__Id_Director_Ejecutivo_Mantenimiento_y_Seguridad",
               table: "Anexo3",
               column: "Id_Director_Ejecutivo_Mantenimiento_y_Seguridad"
               );
            migrationBuilder.CreateIndex(
               name: "FK_Anexo3__Anexo2",
               table: "Anexo3",
               column: "Id_Anexo2"
               );
            #endregion

            #region Anexo4

            migrationBuilder.CreateTable(
                name: "Anexo4",
                columns: table => new
                {
                    Id_Anexo4 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Fecha_Retiro_Cambio_Temporal = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tiempo_Estimado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Propuesta_Inicio_Operacion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Id_Residente = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id_Anexo4);
                    table.ForeignKey(
                      name: "FK_Anexo4__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo1",
                      principalColumn: "Id_PropuestaCambio",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Anexo3",
                      column: x => x.Id_Anexo3,
                      principalTable: "Anexo3",
                      principalColumn: "Id_Anexo3",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Residente",
                      column: x => x.Id_Residente,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Anexo1",
               table: "Anexo4",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Anexo3",
               table: "Anexo4",
               column: "Id_Anexo3"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo4__Residente",
               table: "Anexo4",
               column: "Id_Residente"
               );
            #endregion

            #region Anexo5

            migrationBuilder.CreateTable(
                name: "Anexo5",
                columns: table => new
                {
                    Id_Anexo5 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Responsable_Cambio_Temporal = table.Column<int>(type: "int", nullable: true),

                    Dia_Cambio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mes_Cambio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio_Cambio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dia_Retiro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mes_Retiro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Anio_Retiro = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),

                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id_Anexo5);
                    table.ForeignKey(
                      name: "FK_Anexo5__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo1",
                      principalColumn: "Id_PropuestaCambio",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo4__Id_Responsable_Cambio_Temporal",
                      column: x => x.Id_Responsable_Cambio_Temporal,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo5__Anexo3",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo3",
                      principalColumn: "Id_Anexo3",
                      onDelete: ReferentialAction.SetNull
                    );
                    
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Anexo1",
               table: "Anexo5",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Id_Responsable_Cambio_Temporal",
               table: "Anexo5",
               column: "Id_Responsable_Cambio_Temporal"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo5__Anexo3",
               table: "Anexo5",
               column: "Id_Anexo3"
               );
            #endregion

            #region Anexo6

            migrationBuilder.CreateTable(
                name: "Anexo6",
                columns: table => new
                {
                    Id_Anexo6 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha_Recepcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo1 = table.Column<int>(type: "int", nullable: true),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Fecha_Inicio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Termino = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo4", x => x.Id_Anexo6);
                    table.ForeignKey(
                      name: "FK_Anexo6__Anexo1",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo1",
                      principalColumn: "Id_PropuestaCambio",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo6__Anexo3",
                      column: x => x.Id_Anexo1,
                      principalTable: "Anexo3",
                      principalColumn: "Id_Anexo3",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6__Anexo1",
               table: "Anexo6",
               column: "Id_Anexo1"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6__Anexo3",
               table: "Anexo6",
               column: "Id_Anexo3"
               );
            #endregion

            #region Anexo3_Documentacion

            migrationBuilder.CreateTable(
                name: "Anexo3_Documentacion",
                columns: table => new
                {
                    Id_Anexo3_Documentacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo3 = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Check = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Responsable = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo3_Documentacion", x => x.Id_Anexo3_Documentacion);
                    table.ForeignKey(
                      name: "FK_Anexo3_Documentacion__Anexo3",
                      column: x => x.Id_Anexo3,
                      principalTable: "Anexo3",
                      principalColumn: "Id_Anexo3",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                      name: "FK_Anexo3_Documentacion__Id_Responsable",
                      column: x => x.Id_Responsable,
                      principalTable: "Usuarios",
                      principalColumn: "Id_Usuario",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo3_Documentacion__Anexo3",
               table: "Anexo3_Documentacion",
               column: "Id_Anexo3"
               );
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6__Id_Responsable",
               table: "Anexo3_Documentacion",
               column: "Id_Responsable"
               );
            #endregion

            #region Anexo6_Documentacion

            migrationBuilder.CreateTable(
                name: "Anexo6_Documentacion",
                columns: table => new
                {
                    Id_Anexo6_Documentacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Anexo6 = table.Column<int>(type: "int", nullable: true),
                    Elemento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Check = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Seccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo6_Documentacion", x => x.Id_Anexo6_Documentacion);
                    table.ForeignKey(
                      name: "FK_Anexo6_Documentacion__Anexo6",
                      column: x => x.Id_Anexo6,
                      principalTable: "Anexo6",
                      principalColumn: "Id_Anexo6",
                      onDelete: ReferentialAction.SetNull
                    );

                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_Anexo6_Documentacion__Anexo6",
               table: "Anexo6_Documentacion",
               column: "Id_Anexo6"
               );

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADC");

            migrationBuilder.DropTable(
                name: "ADC_Actividades");

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
                name: "Anexo1");

            migrationBuilder.DropTable(
                name: "Anexo2");

            migrationBuilder.DropTable(
                name: "Anexo3");

            migrationBuilder.DropTable(
                name: "Anexo3_Documentacion");

            migrationBuilder.DropTable(
                name: "Anexo4");

            migrationBuilder.DropTable(
                name: "Anexo5");

            migrationBuilder.DropTable(
                name: "Anexo6");

            migrationBuilder.DropTable(
                name: "Anexo6_Documentacion");

            migrationBuilder.DropTable(
                name: "Anexos");

            migrationBuilder.DropTable(
                name: "Gasoductos");

            migrationBuilder.DropTable(
                name: "Instalaciones");

            migrationBuilder.DropTable(
                name: "Proyecto_Miembros");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Residencias");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tramos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
