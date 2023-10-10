using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    public partial class UserMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SavedOfferId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "SavedOfferList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedOfferList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedOfferList_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedOfferList_OfferList_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OfferList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedOfferList_OfferId",
                table: "SavedOfferList",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedOfferList_UserId1",
                table: "SavedOfferList",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedOfferList");

            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "OfferList",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavedOfferId",
                table: "AspNetUsers",
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
    }
}
