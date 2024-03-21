using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Strawberry.Server.Database.Migrations
{
    public partial class _0010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member_ConfirmImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary Key")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key To Member"),
                    ImageType = table.Column<int>(type: "int", nullable: false, comment: "이미지 타입"),
                    ImageId = table.Column<int>(type: "int", nullable: true, comment: "이미지 Primary Key"),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true, comment: "이미지 경로")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "등록시간")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_ConfirmImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_ConfirmImage_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "승인 대상 이미지")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ConfirmImage_MemberId",
                table: "Member_ConfirmImage",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member_ConfirmImage");
        }
    }
}
