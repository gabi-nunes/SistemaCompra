﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaCompra.Persistence;

namespace SistemaCompra.Persistence.Migrations
{
    [DbContext(typeof(GoodPlaceContext))]
<<<<<<< HEAD:Back/src/SistemaCompra.Persistence/Migrations/20210604005323_Initial.Designer.cs
    [Migration("20210604005323_Initial")]
    partial class Initial
=======
    [Migration("20210605000927_init")]
    partial class init
>>>>>>> 2394f525ebf8725d964cff79412c075e2ecb9e31:Back/src/SistemaCompra.Persistence/Migrations/20210605000927_init.Designer.cs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("SistemaCompra.Domain.Cotacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CotadorId")
                        .HasColumnType("int");

                    b.Property<string>("DataEmissaoCotacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataEntrega")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FornecedorGanhadorId")
                        .HasColumnType("int");

                    b.Property<double>("Frete")
                        .HasColumnType("double");

                    b.Property<int>("FrmPagamento")
                        .HasColumnType("int");

                    b.Property<int>("Parcelas")
                        .HasColumnType("int");

                    b.Property<string>("PrazoOfertas")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("SolicitacaoId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("double");

                    b.Property<int>("fornecedorId")
                        .HasColumnType("int");

                    b.Property<int>("prazoDias")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoId");

                    b.HasIndex("fornecedorId");

                    b.ToTable("Cotacoes");
                });

            modelBuilder.Entity("SistemaCompra.Domain.FamiliaProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("FamiliaProdutos");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CEP")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CNPJ")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Celular")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Cidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Complemento")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Endereco")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FamiliaProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("InscricaoEstadual")
                        .HasColumnType("int");

                    b.Property<int>("InscricaoMunicipal")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("PontuacaoRanking")
                        .HasColumnType("int");

                    b.Property<int>("Posicao")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FamiliaProdutoId");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("SistemaCompra.Domain.ItemCotacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdCotacao")
                        .HasColumnType("int");

                    b.Property<int>("IdProduto")
                        .HasColumnType("int");

                    b.Property<int>("IdSolicitacaoProduto")
                        .HasColumnType("int");

                    b.Property<double>("PrecoUnit")
                        .HasColumnType("double");

                    b.Property<int>("QtdeProduto")
                        .HasColumnType("int");

                    b.Property<int?>("SolicitacaoProdutoId")
                        .HasColumnType("int");

                    b.Property<double>("TotalItem")
                        .HasColumnType("double");

                    b.Property<int>("cotacaoId")
                        .HasColumnType("int");

                    b.Property<int>("itemPedidoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SolicitacaoProdutoId");

                    b.HasIndex("cotacaoId");

                    b.ToTable("itensCotacao");
                });

            modelBuilder.Entity("SistemaCompra.Domain.ItemPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdProduto")
                        .HasColumnType("int");

                    b.Property<int?>("ItemCotacao")
                        .HasColumnType("int");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<double>("PrecoUnit")
                        .HasColumnType("double");

                    b.Property<int>("QtdeProduto")
                        .HasColumnType("int");

                    b.Property<double>("TotalItem")
                        .HasColumnType("double");

                    b.Property<int>("itemCotacaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemCotacao")
                        .IsUnique();

                    b.HasIndex("PedidoId");

                    b.ToTable("itensPedido");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AprovadorId")
                        .HasColumnType("int");

                    b.Property<string>("DataAprovacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataEmissao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ObservacaoRejeicao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("StatusAprov")
                        .HasColumnType("int");

                    b.Property<int>("cotacaoId")
                        .HasColumnType("int");

                    b.Property<double>("valorMaximo")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("cotacaoId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FamiliaProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("UnidMedida")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FamiliaProdutoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Solicitacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DataAprovacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataNecessidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DataSolicitacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("IdAprovador")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ObservacaoRejeicao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("StatusAprovacao")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Solicitacoes");
                });

            modelBuilder.Entity("SistemaCompra.Domain.SolicitacaoProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Produto_Id")
                        .HasColumnType("int");

                    b.Property<int>("QtdeProduto")
                        .HasColumnType("int");

                    b.Property<int>("Solicitacao_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Produto_Id");

                    b.HasIndex("Solicitacao_Id");

                    b.ToTable("solicitacaoProduto");
                });

            modelBuilder.Entity("SistemaCompra.Domain.user", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Cargo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Setor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Cotacao", b =>
                {
                    b.HasOne("SistemaCompra.Domain.Solicitacao", "Solicitacao")
                        .WithMany("Cotacoes")
                        .HasForeignKey("SolicitacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaCompra.Domain.Fornecedor", "Fornecedor")
                        .WithMany("Cotacoes")
                        .HasForeignKey("fornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Fornecedor", b =>
                {
                    b.HasOne("SistemaCompra.Domain.FamiliaProduto", "FamiliaProduto")
                        .WithMany("Fornecedores")
                        .HasForeignKey("FamiliaProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FamiliaProduto");
                });

            modelBuilder.Entity("SistemaCompra.Domain.ItemCotacao", b =>
                {
                    b.HasOne("SistemaCompra.Domain.SolicitacaoProduto", "SolicitacaoProduto")
                        .WithMany()
                        .HasForeignKey("SolicitacaoProdutoId");

                    b.HasOne("SistemaCompra.Domain.Cotacao", "Cotacao")
                        .WithMany("ItensCotacao")
                        .HasForeignKey("cotacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cotacao");

                    b.Navigation("SolicitacaoProduto");
                });

            modelBuilder.Entity("SistemaCompra.Domain.ItemPedido", b =>
                {
                    b.HasOne("SistemaCompra.Domain.ItemCotacao", "itemCotacao")
                        .WithOne("itemPedido")
                        .HasForeignKey("SistemaCompra.Domain.ItemPedido", "ItemCotacao");

                    b.HasOne("SistemaCompra.Domain.Pedido", "Pedido")
                        .WithMany("itensPedidos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("itemCotacao");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Pedido", b =>
                {
                    b.HasOne("SistemaCompra.Domain.Cotacao", "cotacao")
                        .WithMany()
                        .HasForeignKey("cotacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cotacao");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Produto", b =>
                {
                    b.HasOne("SistemaCompra.Domain.FamiliaProduto", null)
                        .WithMany("Produtos")
                        .HasForeignKey("FamiliaProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaCompra.Domain.Solicitacao", b =>
                {
                    b.HasOne("SistemaCompra.Domain.user", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SistemaCompra.Domain.SolicitacaoProduto", b =>
                {
                    b.HasOne("SistemaCompra.Domain.Produto", "Produto")
                        .WithMany("SolicitacaoProdutos")
                        .HasForeignKey("Produto_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaCompra.Domain.Solicitacao", "Solicitacao")
                        .WithMany("SolicitacaoProdutos")
                        .HasForeignKey("Solicitacao_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Solicitacao");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Cotacao", b =>
                {
                    b.Navigation("ItensCotacao");
                });

            modelBuilder.Entity("SistemaCompra.Domain.FamiliaProduto", b =>
                {
                    b.Navigation("Fornecedores");

                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Fornecedor", b =>
                {
                    b.Navigation("Cotacoes");
                });

            modelBuilder.Entity("SistemaCompra.Domain.ItemCotacao", b =>
                {
                    b.Navigation("itemPedido");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Pedido", b =>
                {
                    b.Navigation("itensPedidos");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Produto", b =>
                {
                    b.Navigation("SolicitacaoProdutos");
                });

            modelBuilder.Entity("SistemaCompra.Domain.Solicitacao", b =>
                {
                    b.Navigation("Cotacoes");

                    b.Navigation("SolicitacaoProdutos");
                });
#pragma warning restore 612, 618
        }
    }
}
