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
                      name: "FK_ADC_ADCArchivos",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id_ADC",
                      onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_ADC_ADCArchivos",
               table: "ADC_Archivos",
               column: "Id_ADC"
               );

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

            migrationBuilder.CreateTable(
                name: "ADC_EquipoVerificador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_ADC = table.Column<int>(type: "int", nullable: true),
                    Id_Usuario = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_EquipoVerificador", x => x.Id);
                    table.ForeignKey(
                      name: "FK_ADC_EquipoVerificador",
                      column: x => x.Id_ADC,
                      principalTable: "ADC",
                      principalColumn: "Id_ADC",
                      onDelete: ReferentialAction.SetNull
                    );
                    table.ForeignKey(
                       name: "FK_Usuario_EquipoVerificador",
                       column: x => x.Id_Usuario,
                       principalTable: "Usuarios",
                       principalColumn: "Id_Usuario",
                       onDelete: ReferentialAction.SetNull
                   );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
               name: "Index_ADC_EquipoVerificador",
               table: "ADC_EquipoVerificador",
               column: "Id_ADC"
               );
            migrationBuilder.CreateIndex(
              name: "Index_Usuario_EquipoVerificador",
              table: "ADC_EquipoVerificador",
              column: "Id_Usuario"
              );

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
                    Registro_Eliminado = table.Column<int>(type: "int", nullable: true)
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
                       principalTable: "Usuarios",
                       principalColumn: "Id_Usuario",
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

            migrationBuilder.CreateTable(
                name: "Gasoductos",
                columns: table => new
                {
                    Id_Instalacion = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Gasoductos", x => x.Id_Instalacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "ADC_EquipoVerificador");

            migrationBuilder.DropTable(
                name: "ADC_Normativas");

            migrationBuilder.DropTable(
                name: "ADC_Procesos");

            migrationBuilder.DropTable(
                name: "Anexo1");

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
