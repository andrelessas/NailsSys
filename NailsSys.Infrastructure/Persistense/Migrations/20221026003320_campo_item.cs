using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailsSys.Infrastructure.Persistense.Migrations
{
    public partial class campo_item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAtendimento_Atendimento_AtendimentoId",
                table: "ItemAtendimento");

            migrationBuilder.DropIndex(
                name: "IX_ItemAtendimento_AtendimentoId",
                table: "ItemAtendimento");

            migrationBuilder.DropColumn(
                name: "AtendimentoId",
                table: "ItemAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAtendimento_IdAtendimento",
                table: "ItemAtendimento",
                column: "IdAtendimento");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAtendimento_IdProduto",
                table: "ItemAtendimento",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAtendimento_Atendimento_IdAtendimento",
                table: "ItemAtendimento",
                column: "IdAtendimento",
                principalTable: "Atendimento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAtendimento_Produto_IdProduto",
                table: "ItemAtendimento",
                column: "IdProduto",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAtendimento_Atendimento_IdAtendimento",
                table: "ItemAtendimento");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAtendimento_Produto_IdProduto",
                table: "ItemAtendimento");

            migrationBuilder.DropIndex(
                name: "IX_ItemAtendimento_IdAtendimento",
                table: "ItemAtendimento");

            migrationBuilder.DropIndex(
                name: "IX_ItemAtendimento_IdProduto",
                table: "ItemAtendimento");

            migrationBuilder.AddColumn<int>(
                name: "AtendimentoId",
                table: "ItemAtendimento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAtendimento_AtendimentoId",
                table: "ItemAtendimento",
                column: "AtendimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAtendimento_Atendimento_AtendimentoId",
                table: "ItemAtendimento",
                column: "AtendimentoId",
                principalTable: "Atendimento",
                principalColumn: "Id");
        }
    }
}
