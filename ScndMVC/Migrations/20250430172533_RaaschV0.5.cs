using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class RaaschV05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servicos_IdServicoID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Funcionario_FuncionarioID",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.RenameTable(
                name: "Servicos",
                newName: "Servico");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_FuncionarioID",
                table: "Servico",
                newName: "IX_Servico_FuncionarioID");

            migrationBuilder.AddColumn<bool>(
                name: "Administrador",
                table: "Funcionario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCriacao",
                table: "Funcionario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DtModificao",
                table: "Funcionario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdCriador",
                table: "Funcionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdModificador",
                table: "Funcionario",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HrPausaInicio",
                table: "Configuracao",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HrPausaFim",
                table: "Configuracao",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HrInicio",
                table: "Configuracao",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HrFim",
                table: "Configuracao",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCriacao",
                table: "Configuracao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DtModificao",
                table: "Configuracao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdCriador",
                table: "Configuracao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdModificador",
                table: "Configuracao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HrAgendamento",
                table: "Agendamento",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "IdCriador",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdModificador",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCriacao",
                table: "Servico",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DtModificao",
                table: "Servico",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdCriador",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdModificador",
                table: "Servico",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servico",
                table: "Servico",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_IdServicoID",
                table: "Agendamento",
                column: "IdServicoID",
                principalTable: "Servico",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioID",
                table: "Servico",
                column: "FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servico_IdServicoID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioID",
                table: "Servico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servico",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "Administrador",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "DtCriacao",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "DtModificao",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "IdCriador",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "IdModificador",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "DtCriacao",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "DtModificao",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "IdCriador",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "IdModificador",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "IdCriador",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "IdModificador",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "DtCriacao",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "DtModificao",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "IdCriador",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "IdModificador",
                table: "Servico");

            migrationBuilder.RenameTable(
                name: "Servico",
                newName: "Servicos");

            migrationBuilder.RenameIndex(
                name: "IX_Servico_FuncionarioID",
                table: "Servicos",
                newName: "IX_Servicos_FuncionarioID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HrPausaInicio",
                table: "Configuracao",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HrPausaFim",
                table: "Configuracao",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HrInicio",
                table: "Configuracao",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HrFim",
                table: "Configuracao",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "HrAgendamento",
                table: "Agendamento",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCriadorID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacaoID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioCriadorID",
                table: "Agendamento",
                column: "UsuarioCriadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioModificacaoID",
                table: "Agendamento",
                column: "UsuarioModificacaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servicos_IdServicoID",
                table: "Agendamento",
                column: "IdServicoID",
                principalTable: "Servicos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioCriadorID",
                table: "Agendamento",
                column: "UsuarioCriadorID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioModificacaoID",
                table: "Agendamento",
                column: "UsuarioModificacaoID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Funcionario_FuncionarioID",
                table: "Servicos",
                column: "FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
