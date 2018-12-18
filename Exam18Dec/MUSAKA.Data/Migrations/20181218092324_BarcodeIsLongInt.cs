using Microsoft.EntityFrameworkCore.Migrations;

namespace MUSAKA.Data.Migrations
{
    public partial class BarcodeIsLongInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Barcode",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Barcode",
                table: "Products",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
