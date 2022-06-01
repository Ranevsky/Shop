using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Shop.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    public partial class Add_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristic_Products_ProductId",
                table: "Characteristic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characteristic",
                table: "Characteristic");

            migrationBuilder.RenameTable(
                name: "Characteristic",
                newName: "Characteristics");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristic_ProductId",
                table: "Characteristics",
                newName: "IX_Characteristics_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characteristics",
                table: "Characteristics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Products_ProductId",
                table: "Characteristics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characteristics",
                table: "Characteristics");

            migrationBuilder.RenameTable(
                name: "Characteristics",
                newName: "Characteristic");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_ProductId",
                table: "Characteristic",
                newName: "IX_Characteristic_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characteristic",
                table: "Characteristic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristic_Products_ProductId",
                table: "Characteristic",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
