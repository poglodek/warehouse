using Microsoft.EntityFrameworkCore.Migrations;

namespace warehouse.Database.Migrations
{
    public partial class AddingWhoCreatedInItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhoCreatedId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_WhoCreatedId",
                table: "Items",
                column: "WhoCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_WhoCreatedId",
                table: "Items",
                column: "WhoCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_WhoCreatedId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_WhoCreatedId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WhoCreatedId",
                table: "Items");
        }
    }
}
