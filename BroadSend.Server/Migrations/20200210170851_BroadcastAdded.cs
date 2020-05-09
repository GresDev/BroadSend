using Microsoft.EntityFrameworkCore.Migrations;

namespace BroadSend.Server.Migrations
{
    public partial class BroadcastAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Broadcasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 64, nullable: false),
                    Anons = table.Column<string>(maxLength: 2048, nullable: false),
                    DateAired = table.Column<decimal>(nullable: false),
                    DateAiredEnd = table.Column<decimal>(nullable: false),
                    Vendor = table.Column<string>(maxLength: 512, nullable: false),
                    Author = table.Column<string>(maxLength: 2048, nullable: false),
                    Composer = table.Column<string>(maxLength: 512, nullable: false),
                    Director = table.Column<string>(maxLength: 256, nullable: false),
                    Fragment = table.Column<string>(maxLength: 2048, nullable: false),
                    Presenter = table.Column<string>(maxLength: 2048, nullable: false),
                    Guest = table.Column<string>(maxLength: 2048, nullable: false),
                    Country = table.Column<string>(maxLength: 256, nullable: false),
                    Language = table.Column<string>(maxLength: 64, nullable: false),
                    FileName = table.Column<string>(maxLength: 512, nullable: false),
                    FileSHA256 = table.Column<string>(maxLength: 64, nullable: false),
                    Transmitted = table.Column<bool>(nullable: false),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broadcasts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Broadcasts");
        }
    }
}
