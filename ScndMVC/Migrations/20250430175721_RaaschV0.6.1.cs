using Microsoft.EntityFrameworkCore.Migrations;

namespace ScndMVC.Migrations
{
    public partial class RaaschV061 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DsServico",
                table: "Servico",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(63) CHARACTER SET utf8mb4",
                oldMaxLength: 63,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DsServico",
                table: "Servico",
                type: "varchar(63) CHARACTER SET utf8mb4",
                maxLength: 63,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
