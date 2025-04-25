using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class RaaschV04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_IdUsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_IdUsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_IdUsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_IdUsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "IdUsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "IdUsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioCriadorID",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModificacaoID",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioCriadorID",
                table: "Agendamento",
                column: "UsuarioCriadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioModificacaoID",
                table: "Agendamento",
                column: "UsuarioModificacaoID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "UsuarioCriadorID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacaoID",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioCriadorID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioModificacaoID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdUsuarioCriadorID",
                table: "Agendamento",
                column: "IdUsuarioCriadorID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdUsuarioModificacaoID",
                table: "Agendamento",
                column: "IdUsuarioModificacaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_IdUsuarioCriadorID",
                table: "Agendamento",
                column: "IdUsuarioCriadorID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_IdUsuarioModificacaoID",
                table: "Agendamento",
                column: "IdUsuarioModificacaoID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
