using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class OfferMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "OfferList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfferList_CreatorId",
                table: "OfferList",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferList_AspNetUsers_CreatorId",
                table: "OfferList",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferList_AspNetUsers_CreatorId",
                table: "OfferList");

            migrationBuilder.DropIndex(
                name: "IX_OfferList_CreatorId",
                table: "OfferList");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "OfferList");
        }
    }
}
