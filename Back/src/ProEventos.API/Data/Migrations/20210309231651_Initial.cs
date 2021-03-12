using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProEventos.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Local = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DataEvento = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Tema = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    QtdePessoas = table.Column<int>(type: "int", nullable: false),
                    Lote = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ImagemURL = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
