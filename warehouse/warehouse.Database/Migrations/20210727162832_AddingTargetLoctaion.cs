using Microsoft.EntityFrameworkCore.Migrations;

namespace warehouse.Database.Migrations
{
    public partial class AddingTargetLoctaion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetLocation",
                table: "ShippingInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetLocation",
                table: "ShippingInfos");
        }
    }
}
