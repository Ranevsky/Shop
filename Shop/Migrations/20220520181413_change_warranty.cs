using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    public partial class change_warranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "WarrantyId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarrantyId",
                table: "Products",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WarrantyId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WarrantyId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Warranty",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }
    }
}
