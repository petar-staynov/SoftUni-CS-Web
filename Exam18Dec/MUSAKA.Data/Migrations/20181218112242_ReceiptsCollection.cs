using Microsoft.EntityFrameworkCore.Migrations;

namespace MUSAKA.Data.Migrations
{
    public partial class ReceiptsCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptsOrders_Receipts_ReceiptId",
                table: "ReceiptsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptsOrders_Receipts_ReceiptId",
                table: "ReceiptsOrders",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptsOrders_Receipts_ReceiptId",
                table: "ReceiptsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptsOrders_Receipts_ReceiptId",
                table: "ReceiptsOrders",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
