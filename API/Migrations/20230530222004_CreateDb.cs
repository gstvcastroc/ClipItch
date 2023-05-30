using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clip",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    url = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    embed_url = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    broadcaster_id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    broadcaster_name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    broadcaster_profile_image_url = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    creator_id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    creator_name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    video_id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    game_id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    game_name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    language = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    view_count = table.Column<int>(type: "INTEGER", maxLength: 120, nullable: false),
                    created_at = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    thumbnail_url = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    duration = table.Column<decimal>(type: "numeric(38,17)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clip", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    box_art_url = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clip");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
