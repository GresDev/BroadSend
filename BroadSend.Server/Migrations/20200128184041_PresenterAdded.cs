using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadSend.Server.Migrations
{
    public partial class PresenterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presenters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presenter_Aliases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(maxLength: 64, nullable: false),
                    PresenterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presenter_Aliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presenter_Aliases_Presenters_PresenterId",
                        column: x => x.PresenterId,
                        principalTable: "Presenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Presenter_Aliases_PresenterId",
                table: "Presenter_Aliases",
                column: "PresenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presenter_Aliases");

            migrationBuilder.DropTable(
                name: "Presenters");
        }
    }
}
