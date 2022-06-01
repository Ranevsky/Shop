using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Shop.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    public partial class Delete_Characteristic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Characteristic",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Characteristic",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }
    }
}
