using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable IDE0161 // Convert to file-scoped namespace
namespace Shop.Migrations
#pragma warning restore IDE0161 // Convert to file-scoped namespace
{
    public partial class Add_DescriptionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescriptionClassId",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Description",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Description", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DescriptionClassId",
                table: "Products",
                column: "DescriptionClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Description_DescriptionClassId",
                table: "Products",
                column: "DescriptionClassId",
                principalTable: "Description",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Description_DescriptionClassId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Description");

            migrationBuilder.DropIndex(
                name: "IX_Products_DescriptionClassId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionClassId",
                table: "Products");
        }
    }
}
