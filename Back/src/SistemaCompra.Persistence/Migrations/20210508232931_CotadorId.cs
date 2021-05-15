using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCompra.Persistence.Migrations
{
    public partial class CotadorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CotadorId",
                table: "Cotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CotadorId",
                table: "Cotacoes");
        }
    }
}
