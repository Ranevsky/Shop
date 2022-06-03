using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations;

public partial class addIsStockfield : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Image_Products_ProductId",
            table: "Image");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Image",
            table: "Image");

        migrationBuilder.RenameTable(
            name: "Image",
            newName: "Images");

        migrationBuilder.RenameIndex(
            name: "IX_Image_ProductId",
            table: "Images",
            newName: "IX_Images_ProductId");

        migrationBuilder.AddColumn<bool>(
            name: "IsStock",
            table: "Products",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Images",
            table: "Images",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Images_Products_ProductId",
            table: "Images",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Images_Products_ProductId",
            table: "Images");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Images",
            table: "Images");

        migrationBuilder.DropColumn(
            name: "IsStock",
            table: "Products");

        migrationBuilder.RenameTable(
            name: "Images",
            newName: "Image");

        migrationBuilder.RenameIndex(
            name: "IX_Images_ProductId",
            table: "Image",
            newName: "IX_Image_ProductId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Image",
            table: "Image",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Image_Products_ProductId",
            table: "Image",
            column: "ProductId",
            principalTable: "Products",
            principalColumn: "Id");
    }
}