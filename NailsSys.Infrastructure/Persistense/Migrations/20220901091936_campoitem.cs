using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailsSys.Infrastructure.Persistense.Migrations
{
    public partial class campoitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Item",
                table: "ItemAgendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "ItemAgendamento");
        }
    }
}
