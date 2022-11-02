using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailsSys.Infrastructure.Persistense.Migrations
{
    public partial class campo_cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Cliente_ClienteId",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_ClienteId",
                table: "Atendimento");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Atendimento");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_IdCliente",
                table: "Atendimento",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Cliente_IdCliente",
                table: "Atendimento",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimento_Cliente_IdCliente",
                table: "Atendimento");

            migrationBuilder.DropIndex(
                name: "IX_Atendimento_IdCliente",
                table: "Atendimento");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Atendimento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_ClienteId",
                table: "Atendimento",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimento_Cliente_ClienteId",
                table: "Atendimento",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");
        }
    }
}
