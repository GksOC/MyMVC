using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class RaaschV06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servico_IdServicoID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_IdServicoID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "IdServicoID",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "ServicoID",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ServicoID",
                table: "Agendamento",
                column: "ServicoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento",
                column: "ServicoID",
                principalTable: "Servico",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Servico_ServicoID",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_ServicoID",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "ServicoID",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "IdServicoID",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdServicoID",
                table: "Agendamento",
                column: "IdServicoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Servico_IdServicoID",
                table: "Agendamento",
                column: "IdServicoID",
                principalTable: "Servico",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
