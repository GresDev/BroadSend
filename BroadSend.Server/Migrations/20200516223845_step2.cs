using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadSend.Server.Migrations
{
    public partial class step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PresenterAliases_Presenters_PresenterId",
                table: "PresenterAliases");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleAliases_Titles_TitleId",
                table: "TitleAliases");

            migrationBuilder.DropIndex(
                name: "IX_TitleAliases_TitleId",
                table: "TitleAliases");

            migrationBuilder.DropIndex(
                name: "IX_PresenterAliases_PresenterId",
                table: "PresenterAliases");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "TitleAliases");

            migrationBuilder.DropColumn(
                name: "PresenterId",
                table: "PresenterAliases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitleId",
                table: "TitleAliases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PresenterId",
                table: "PresenterAliases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitleAliases_TitleId",
                table: "TitleAliases",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_PresenterAliases_PresenterId",
                table: "PresenterAliases",
                column: "PresenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PresenterAliases_Presenters_PresenterId",
                table: "PresenterAliases",
                column: "PresenterId",
                principalTable: "Presenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleAliases_Titles_TitleId",
                table: "TitleAliases",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
