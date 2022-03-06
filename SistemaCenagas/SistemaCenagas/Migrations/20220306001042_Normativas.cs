using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCenagas.Migrations
{
    public partial class Normativas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "ADC_Actividades");

            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    Id_Anexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id_Anexo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ADC_Normativas",
                columns: table => new
                {
                    Id_Normativa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Actividad = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsable = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Anexo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADC_Normativas", x => x.Id_Normativa);
                    table.ForeignKey(
                        name: "FK_Anexo1",
                        column: x => x.Id_Anexo,
                        principalTable: "Anexos",
                        principalColumn: "Id_Anexo",
                        onDelete: ReferentialAction.SetNull
                    );
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.CreateIndex(
                name: "Index_Anexo1",
                table: "ADC_Normativas",
                column: "Id_Anexo"
                );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADC_Normativas");

            migrationBuilder.DropTable(
                name: "Anexos");

            migrationBuilder.DropColumn(
                name: "Id_Proyecto",
                table: "ADC");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "ADC_Actividades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
