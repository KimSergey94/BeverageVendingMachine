using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageVendingMachine.Infrastructure.Migrations
{
    public partial class StorageItemIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StorageItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StorageItems");
        }
    }
}
