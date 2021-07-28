using Microsoft.EntityFrameworkCore.Migrations;

namespace warehouse.Database.Migrations
{
    public partial class AddinWhoCreatedToShippingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhoCreatedId",
                table: "ShippingInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingInfos_WhoCreatedId",
                table: "ShippingInfos",
                column: "WhoCreatedId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfos_Users_WhoCreatedId",
                table: "ShippingInfos",
                column: "WhoCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfos_Users_WhoCreatedId",
                table: "ShippingInfos");

            migrationBuilder.DropIndex(
                name: "IX_ShippingInfos_WhoCreatedId",
                table: "ShippingInfos");

            migrationBuilder.DropColumn(
                name: "WhoCreatedId",
                table: "ShippingInfos");
        }
    }
}
