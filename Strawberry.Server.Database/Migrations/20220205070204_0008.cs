using Microsoft.EntityFrameworkCore.Migrations;

namespace Strawberry.Server.Database.Migrations
{
    public partial class _0008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "ADData",
                type: "longtext",
                nullable: true,
                comment: "추가 데이터 (Json)",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "추가 데이터 (Json)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JsonData",
                table: "ADData",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                comment: "추가 데이터 (Json)",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "추가 데이터 (Json)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
