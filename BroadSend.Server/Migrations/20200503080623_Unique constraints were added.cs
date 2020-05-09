using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadSend.Server.Migrations
{
    public partial class Uniqueconstraintswereadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_Name",
                table: "Vendors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_Name",
                table: "Titles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitleAliases_Alias",
                table: "TitleAliases",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presenters_FullName",
                table: "Presenters",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PresenterAliases_Alias",
                table: "PresenterAliases",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_FullName",
                table: "Directors",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Composers_Name",
                table: "Composers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendors_Name",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Titles_Name",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_TitleAliases_Alias",
                table: "TitleAliases");

            migrationBuilder.DropIndex(
                name: "IX_Presenters_FullName",
                table: "Presenters");

            migrationBuilder.DropIndex(
                name: "IX_PresenterAliases_Alias",
                table: "PresenterAliases");

            migrationBuilder.DropIndex(
                name: "IX_Languages_Name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Directors_FullName",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Composers_Name",
                table: "Composers");

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Alias", "FullName" },
                values: new object[,]
                {
                    { 1, "Антипов", "Антипов Василий Владимирович" },
                    { 2, "Бурсина", "Бурсина Мария Владимировна" },
                    { 3, "Лелякова", "Лелякова  Марина Георгиевна" },
                    { 4, "Ростовцева", "Ростовцева" }
                });
        }
    }
}
