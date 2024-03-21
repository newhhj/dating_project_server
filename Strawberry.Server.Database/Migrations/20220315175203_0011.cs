using Microsoft.EntityFrameworkCore.Migrations;

namespace Strawberry.Server.Database.Migrations
{
    public partial class _0011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Member_ConfirmImage",
                type: "int",
                nullable: true,
                comment: "주 컨텐츠 아이디");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Member_ConfirmImage");
        }
    }
}
