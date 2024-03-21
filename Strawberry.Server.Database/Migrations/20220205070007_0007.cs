using Microsoft.EntityFrameworkCore.Migrations;

namespace Strawberry.Server.Database.Migrations
{
    public partial class _0007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "ADData",
                type: "longtext",
                nullable: false,
                comment: "추가 데이터 (Json)",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "광고 데이터 (Json)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ADName",
                table: "ADData",
                type: "longtext",
                nullable: false,
                comment: "광고명")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "ADData",
                type: "longtext",
                nullable: true,
                comment: "연결링크")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADName",
                table: "ADData");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "ADData");

            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "ADData",
                type: "longtext",
                nullable: false,
                comment: "광고 데이터 (Json)",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "추가 데이터 (Json)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
