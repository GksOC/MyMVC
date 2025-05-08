using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class V12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioID",
                table: "Servico");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioID",
                table: "Servico",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServicoID",
                table: "Agendamento",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NmCliente",
                table: "Agendamento",
                maxLength: 63,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(63) CHARACTER SET utf8mb4",
                oldMaxLength: 63);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento",
                column: "ServicoID",
                principalTable: "Servico",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioID",
                table: "Servico");

            migrationBuilder.AlterColumn<int>(
                name: "FuncionarioID",
                table: "Servico",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ServicoID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NmCliente",
                table: "Agendamento",
                type: "varchar(63) CHARACTER SET utf8mb4",
                maxLength: 63,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 63,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento",
                column: "ServicoID",
                principalTable: "Servico",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servico_Funcionario_FuncionarioID",
                table: "Servico",
                column: "FuncionarioID",
                principalTable: "Funcionario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
