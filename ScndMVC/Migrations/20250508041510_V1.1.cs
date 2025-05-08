using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracao",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtModificao = table.Column<DateTime>(nullable: true),
                    IdCriador = table.Column<int>(nullable: false),
                    IdModificador = table.Column<int>(nullable: true),
                    Domingo = table.Column<bool>(nullable: false),
                    Segunda = table.Column<bool>(nullable: false),
                    Terca = table.Column<bool>(nullable: false),
                    Quarta = table.Column<bool>(nullable: false),
                    Quinta = table.Column<bool>(nullable: false),
                    Sexta = table.Column<bool>(nullable: false),
                    Sabado = table.Column<bool>(nullable: false),
                    PeriodoAtendimento = table.Column<int>(nullable: false),
                    HrInicio = table.Column<TimeSpan>(nullable: false),
                    HrFim = table.Column<TimeSpan>(nullable: false),
                    HrPausaInicio = table.Column<TimeSpan>(nullable: false),
                    HrPausaFim = table.Column<TimeSpan>(nullable: false),
                    AgendaMultipla = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtModificao = table.Column<DateTime>(nullable: true),
                    IdCriador = table.Column<int>(nullable: false),
                    IdModificador = table.Column<int>(nullable: true),
                    NmProfissional = table.Column<string>(maxLength: 63, nullable: false),
                    Telefone = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Login = table.Column<string>(maxLength: 31, nullable: false),
                    Senha = table.Column<string>(maxLength: 255, nullable: false),
                    Administrador = table.Column<bool>(nullable: false),
                    ConfiguracaoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Funcionario_Configuracao_ConfiguracaoID",
                        column: x => x.ConfiguracaoID,
                        principalTable: "Configuracao",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtModificao = table.Column<DateTime>(nullable: true),
                    IdCriador = table.Column<int>(nullable: false),
                    IdModificador = table.Column<int>(nullable: true),
                    NmServico = table.Column<string>(maxLength: 63, nullable: false),
                    DsServico = table.Column<string>(maxLength: 255, nullable: true),
                    Valor = table.Column<float>(nullable: false),
                    FuncionarioID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Servico_Funcionario_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DtCriacao = table.Column<DateTime>(nullable: false),
                    DtModificao = table.Column<DateTime>(nullable: true),
                    IdCriador = table.Column<int>(nullable: false),
                    IdModificador = table.Column<int>(nullable: true),
                    FuncionarioID = table.Column<int>(nullable: false),
                    DtDia = table.Column<DateTime>(nullable: false),
                    HrAgendamento = table.Column<TimeSpan>(nullable: false),
                    NmCliente = table.Column<string>(maxLength: 63, nullable: false),
                    ServicoID = table.Column<int>(nullable: false),
                    Valor = table.Column<float>(nullable: true),
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
                        name: "FK_Agendamento_Servico_ServicoID",
                        column: x => x.ServicoID,
                        principalTable: "Servico",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_FuncionarioID",
                table: "Agendamento",
                column: "FuncionarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ServicoID",
                table: "Agendamento",
                column: "ServicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ConfiguracaoID",
                table: "Funcionario",
                column: "ConfiguracaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_FuncionarioID",
                table: "Servico",
                column: "FuncionarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Configuracao");
        }
    }
}
