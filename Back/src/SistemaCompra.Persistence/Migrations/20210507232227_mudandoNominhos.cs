using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCompra.Persistence.Migrations
{
    public partial class mudandoNominhos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrazoOferta",
                table: "Cotacoes",
                newName: "PrazoOfertas");

            migrationBuilder.RenameColumn(
                name: "PrazoCotacao",
                table: "Cotacoes",
                newName: "DataEmissaoCotacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrazoOfertas",
                table: "Cotacoes",
                newName: "PrazoOferta");

            migrationBuilder.RenameColumn(
                name: "DataEmissaoCotacao",
                table: "Cotacoes",
                newName: "PrazoCotacao");
        }
    }
}
