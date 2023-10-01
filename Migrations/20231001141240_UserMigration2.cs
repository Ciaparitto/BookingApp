using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class UserMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferList_AspNetUsers_CreatorId",
                table: "OfferList");

            migrationBuilder.DropIndex(
                name: "IX_OfferList_CreatorId",
                table: "OfferList");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "OfferList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "OfferList",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
