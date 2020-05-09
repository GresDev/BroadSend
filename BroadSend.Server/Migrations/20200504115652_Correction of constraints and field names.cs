using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadSend.Server.Migrations
{
    public partial class Correctionofconstraintsandfieldnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presenters_FullName",
                table: "Presenters");

            migrationBuilder.DropIndex(
                name: "IX_Directors_FullName",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Presenters");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Directors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Presenters",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Directors",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Presenters_Name",
                table: "Presenters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_Alias",
                table: "Directors",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_Name",
                table: "Directors",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presenters_Name",
                table: "Presenters");

            migrationBuilder.DropIndex(
                name: "IX_Directors_Alias",
                table: "Directors");

            migrationBuilder.DropIndex(
                name: "IX_Directors_Name",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Presenters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Directors");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Presenters",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Directors",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Presenters_FullName",
                table: "Presenters",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Directors_FullName",
                table: "Directors",
                column: "FullName",
                unique: true);
        }
    }
}
