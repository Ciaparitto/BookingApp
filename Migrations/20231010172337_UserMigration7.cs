using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class UserMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedOfferList_AspNetUsers_UserId1",
                table: "SavedOfferList");

            migrationBuilder.DropIndex(
                name: "IX_SavedOfferList_UserId1",
                table: "SavedOfferList");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SavedOfferList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavedOfferList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOfferList_UserId",
                table: "SavedOfferList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedOfferList_AspNetUsers_UserId",
                table: "SavedOfferList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedOfferList_AspNetUsers_UserId",
                table: "SavedOfferList");

            migrationBuilder.DropIndex(
                name: "IX_SavedOfferList_UserId",
                table: "SavedOfferList");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SavedOfferList",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "SavedOfferList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedOfferList_UserId1",
                table: "SavedOfferList",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedOfferList_AspNetUsers_UserId1",
                table: "SavedOfferList",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
