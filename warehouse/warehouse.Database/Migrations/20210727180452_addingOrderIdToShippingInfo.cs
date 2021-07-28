using Microsoft.EntityFrameworkCore.Migrations;

namespace warehouse.Database.Migrations
{
    public partial class addingOrderIdToShippingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ShippingInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingInfos_OrderId",
                table: "ShippingInfos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfos_Orders_OrderId",
                table: "ShippingInfos",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfos_Orders_OrderId",
                table: "ShippingInfos");

            migrationBuilder.DropIndex(
                name: "IX_ShippingInfos_OrderId",
                table: "ShippingInfos");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShippingInfos");
        }
    }
}