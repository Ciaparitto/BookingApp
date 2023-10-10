using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class UserMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "OfferList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "OfferList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OfferList_UserModelId",
                table: "OfferList",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferList_AspNetUsers_UserModelId",
                table: "OfferList",
                column: "UserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferList_AspNetUsers_UserModelId",
                table: "OfferList");

            migrationBuilder.DropIndex(
                name: "IX_OfferList_UserModelId",
                table: "OfferList");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "OfferList");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "OfferList");
        }
    }
}
