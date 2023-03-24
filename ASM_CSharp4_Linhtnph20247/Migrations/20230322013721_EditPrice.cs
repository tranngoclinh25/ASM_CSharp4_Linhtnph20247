using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_CSharp4_Linhtnph20247.Migrations
{
    public partial class EditPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceNew",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "PriceOld",
                table: "Products",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "PriceOld");

            migrationBuilder.AddColumn<float>(
                name: "PriceNew",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
