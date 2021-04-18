﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCompra.Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamiliaProdutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliaProdutos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatusAprov = table.Column<int>(type: "int", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Aprovador = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DataAprovacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observacao = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    cotacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Setor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Senha = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Cargo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNPJ = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IdFamiliaProduto = table.Column<int>(type: "int", nullable: false),
                    Cidade = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Endereco = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Estado = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CEP = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    InscricaoMunicipal = table.Column<int>(type: "int", nullable: false),
                    InscricaoEstadual = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Telefone = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Celular = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PontuacaoRanking = table.Column<int>(type: "int", nullable: false),
                    FamiliaProdId = table.Column<int>(type: "int", nullable: false),
                    Senha = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FamiliaProdutoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedores_FamiliaProdutos_FamiliaProdutoId",
                        column: x => x.FamiliaProdutoId,
                        principalTable: "FamiliaProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UnidMedida = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FamiliaProdId = table.Column<int>(type: "int", nullable: false),
                    FamiliaProdutoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_FamiliaProdutos_FamiliaProdutoId",
                        column: x => x.FamiliaProdutoId,
                        principalTable: "FamiliaProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solcitacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DataNecessidade = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StatusAprovacao = table.Column<int>(type: "int", nullable: false),
                    Aprovador = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CotacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solcitacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solcitacoes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PrazoCotacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SolicitacaoId = table.Column<int>(type: "int", nullable: false),
                    Frete = table.Column<double>(type: "double", nullable: false),
                    FrmPagamento = table.Column<int>(type: "int", nullable: false),
                    PrazoOferta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Parcelas = table.Column<int>(type: "int", nullable: false),
                    FornecedorGanhadorId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "double", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    fornecedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotacoes_Fornecedores_fornecedorId",
                        column: x => x.fornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotacoes_Pedido_Id",
                        column: x => x.Id,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cotacoes_Solcitacoes_Id",
                        column: x => x.Id,
                        principalTable: "Solcitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "solicitacaoProduto",
                columns: table => new
                {
                    Solicitacaoid = table.Column<int>(type: "int", nullable: false),
                    Produtoid = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    QtdeProduto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacaoProduto", x => new { x.Solicitacaoid, x.Produtoid });
                    table.ForeignKey(
                        name: "FK_solicitacaoProduto_Produtos_Produtoid",
                        column: x => x.Produtoid,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_solicitacaoProduto_Solcitacoes_Solicitacaoid",
                        column: x => x.Solicitacaoid,
                        principalTable: "Solcitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itensCotacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCotacao = table.Column<int>(type: "int", nullable: false),
                    IdSolicitacaoProduto = table.Column<int>(type: "int", nullable: false),
                    SolicitacaoProdutoSolicitacaoid = table.Column<int>(type: "int", nullable: true),
                    SolicitacaoProdutoProdutoid = table.Column<int>(type: "int", nullable: true),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    QtdeProduto = table.Column<int>(type: "int", nullable: false),
                    PrecoUnit = table.Column<double>(type: "double", nullable: false),
                    cotacaoId = table.Column<int>(type: "int", nullable: false),
                    itemPedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itensCotacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itensCotacao_Cotacoes_cotacaoId",
                        column: x => x.cotacaoId,
                        principalTable: "Cotacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itensCotacao_solicitacaoProduto_SolicitacaoProdutoSolicitaca~",
                        columns: x => new { x.SolicitacaoProdutoSolicitacaoid, x.SolicitacaoProdutoProdutoid },
                        principalTable: "solicitacaoProduto",
                        principalColumns: new[] { "Solicitacaoid", "Produtoid" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "itensPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    QtdeProduto = table.Column<int>(type: "int", nullable: false),
                    PrecoUnit = table.Column<double>(type: "double", nullable: false),
                    itemCotacaoId = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itensPedido_itensCotacao_Id",
                        column: x => x.Id,
                        principalTable: "itensCotacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itensPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotacoes_fornecedorId",
                table: "Cotacoes",
                column: "fornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_FamiliaProdutoId",
                table: "Fornecedores",
                column: "FamiliaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_itensCotacao_cotacaoId",
                table: "itensCotacao",
                column: "cotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_itensCotacao_SolicitacaoProdutoSolicitacaoid_SolicitacaoProd~",
                table: "itensCotacao",
                columns: new[] { "SolicitacaoProdutoSolicitacaoid", "SolicitacaoProdutoProdutoid" });

            migrationBuilder.CreateIndex(
                name: "IX_itensPedido_PedidoId",
                table: "itensPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FamiliaProdutoId",
                table: "Produtos",
                column: "FamiliaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solcitacoes_UserId",
                table: "Solcitacoes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_solicitacaoProduto_Produtoid",
                table: "solicitacaoProduto",
                column: "Produtoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itensPedido");

            migrationBuilder.DropTable(
                name: "itensCotacao");

            migrationBuilder.DropTable(
                name: "Cotacoes");

            migrationBuilder.DropTable(
                name: "solicitacaoProduto");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Solcitacoes");

            migrationBuilder.DropTable(
                name: "FamiliaProdutos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
