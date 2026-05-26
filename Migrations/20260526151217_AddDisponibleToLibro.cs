using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace javier_api.Migrations
{
    public partial class AddDisponibleToLibro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Libros",
                type: "bit",
                nullable: false,
                defaultValue: true);  // true = disponible por defecto
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Libros");
        }
    }
}