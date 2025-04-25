using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class RaaschV03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfiguracaoID",
                table: "Seller",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NmProfissional = table.Column<string>(maxLength: 63, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Login = table.Column<string>(maxLength: 31, nullable: false),
                    Senha = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FuncionarioID = table.Column<int>(nullable: false),
                    Domingo = table.Column<bool>(nullable: false),
                    Segunda = table.Column<bool>(nullable: false),
                    Terca = table.Column<bool>(nullable: false),
                    Quarta = table.Column<bool>(nullable: false),
                    Quinta = table.Column<bool>(nullable: false),
                    Sexta = table.Column<bool>(nullable: false),
                    Sabado = table.Column<bool>(nullable: false),
                    PeriodoAtendimento = table.Column<int>(nullable: false),
                    HrInicio = table.Column<DateTime>(nullable: false),
                    HrFim = table.Column<DateTime>(nullable: false),
                    HrPausaInicio = table.Column<DateTime>(nullable: false),
                    HrPausaFim = table.Column<DateTime>(nullable: false),
                    AgendaMultipla = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Configuracao_Funcionario_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FuncionarioID = table.Column<int>(nullable: false),
                    NmServico = table.Column<string>(maxLength: 63, nullable: false),
                    DsServico = table.Column<string>(maxLength: 63, nullable: true),
                    Valor = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Servicos_Funcionario_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtModificao = table.Column<DateTime>(nullable: false),
                    IdUsuarioCriadorID = table.Column<int>(nullable: false),
                    IdUsuarioModificacaoID = table.Column<int>(nullable: false),
                    FuncionarioID = table.Column<int>(nullable: false),
                    DtDia = table.Column<DateTime>(nullable: false),
                    HrAgendamento = table.Column<DateTime>(nullable: false),
                    NmCliente = table.Column<string>(maxLength: 63, nullable: false),
                    IdServicoID = table.Column<int>(nullable: false),
                    Valor = table.Column<float>(nullable: false),
                    Stats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Agendamento_Funcionario_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Servicos_IdServicoID",
                        column: x => x.IdServicoID,
                        principalTable: "Servicos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Funcionario_IdUsuarioCriadorID",
                        column: x => x.IdUsuarioCriadorID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Funcionario_IdUsuarioModificacaoID",
                        column: x => x.IdUsuarioModificacaoID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seller_ConfiguracaoID",
                table: "Seller",
                column: "ConfiguracaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_FuncionarioID",
                table: "Agendamento",
                column: "FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdServicoID",
                table: "Agendamento",
                column: "IdServicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdUsuarioCriadorID",
                table: "Agendamento",
                column: "IdUsuarioCriadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdUsuarioModificacaoID",
                table: "Agendamento",
                column: "IdUsuarioModificacaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Configuracao_FuncionarioID",
                table: "Configuracao",
                column: "FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_FuncionarioID",
                table: "Servicos",
                column: "FuncionarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Configuracao_ConfiguracaoID",
                table: "Seller",
                column: "ConfiguracaoID",
                principalTable: "Configuracao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Configuracao_ConfiguracaoID",
                table: "Seller");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Configuracao");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Seller_ConfiguracaoID",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoID",
                table: "Seller");
        }
    }
}
