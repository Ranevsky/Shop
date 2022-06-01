using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Shop.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    public partial class Delete_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Description_DescriptionClassId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DescriptionClassId",
                table: "Products",
                newName: "DescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DescriptionClassId",
                table: "Products",
                newName: "IX_Products_DescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Description_DescriptionId",
                table: "Products",
                column: "DescriptionId",
                principalTable: "Description",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Description_DescriptionId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DescriptionId",
                table: "Products",
                newName: "DescriptionClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_DescriptionId",
                table: "Products",
                newName: "IX_Products_DescriptionClassId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Description_DescriptionClassId",
                table: "Products",
                column: "DescriptionClassId",
                principalTable: "Description",
                principalColumn: "Id");
        }
    }
}
