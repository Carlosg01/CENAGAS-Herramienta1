using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Proyecto_Miembros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proyecto_Miembros");
        }
    }
}
