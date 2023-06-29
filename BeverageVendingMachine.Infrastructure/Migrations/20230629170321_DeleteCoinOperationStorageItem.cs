using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageVendingMachine.Infrastructure.Migrations
{
    public partial class DeleteCoinOperationStorageItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinOperations_StorageItems_StorageItemId",
                table: "CoinOperations");

            migrationBuilder.DropIndex(
                name: "IX_CoinOperations_StorageItemId",
                table: "CoinOperations");

            migrationBuilder.DropColumn(
                name: "StorageItemId",
                table: "CoinOperations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageItemId",
                table: "CoinOperations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoinOperations_StorageItemId",
                table: "CoinOperations",
                column: "StorageItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinOperations_StorageItems_StorageItemId",
                table: "CoinOperations",
                column: "StorageItemId",
                principalTable: "StorageItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
