using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configuracao_Funcionario_FuncionarioID",
                table: "Configuracao");

            migrationBuilder.DropIndex(
                name: "IX_Configuracao_FuncionarioID",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "FuncionarioID",
                table: "Configuracao");

            migrationBuilder.AddColumn<int>(
                name: "ConfiguracaoID",
                table: "Funcionario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_ConfiguracaoID",
                table: "Funcionario",
                column: "ConfiguracaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Configuracao_ConfiguracaoID",
                table: "Funcionario",
                column: "ConfiguracaoID",
                principalTable: "Configuracao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Configuracao_ConfiguracaoID",
                table: "Funcionario");

            migrationBuilder.DropIndex(
                name: "IX_Funcionario_ConfiguracaoID",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoID",
                table: "Funcionario");

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioID",
                table: "Configuracao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Configuracao_FuncionarioID",
                table: "Configuracao",
                column: "FuncionarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Configuracao_Funcionario_FuncionarioID",
                table: "Configuracao",
                column: "FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
