using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlCutter.Migrations
{
    public partial class AddTokenIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Token = table.Column<string>(type: "varchar(767)", nullable: false),
                    LongUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Token);
                });
            migrationBuilder.CreateIndex(
                name: "token_idx",
                table: "Urls",
                column: "Token"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
