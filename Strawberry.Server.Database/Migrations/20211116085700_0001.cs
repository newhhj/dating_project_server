using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Strawberry.Server.Database.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HelpMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpMessage", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passwd = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nickname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "Manager")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Useage = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApiKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberState = table.Column<int>(type: "int", nullable: false),
                    TermCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PrivacyCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LocationCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SensitiveCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MarketingCheckTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nickname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tall = table.Column<int>(type: "int", nullable: false),
                    BodyStyle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    School = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Job = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Religion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alcohol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Smoking = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Personality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Blood = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lat = table.Column<double>(type: "double", nullable: false),
                    Lng = table.Column<double>(type: "double", nullable: false),
                    LevelType = table.Column<int>(type: "int", nullable: false),
                    LevelPoint = table.Column<double>(type: "double", nullable: false),
                    IsRoyal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasStarBadge = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FreeChoiceCount = table.Column<int>(type: "int", nullable: false),
                    FreeChattingCount = table.Column<int>(type: "int", nullable: false),
                    IsVIP = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddChoice3Time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AddChatting1Time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AddChatting3Time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FreeChoiceTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FreeSmartChoiceTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FreeChattingTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FirstMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstVoice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Platform = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PushToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Point = table.Column<int>(type: "int", nullable: false),
                    RateCharming = table.Column<int>(type: "int", nullable: false),
                    RateResponse = table.Column<int>(type: "int", nullable: false),
                    UseNotiRecommand = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiReceiveChoice = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiSendChoiceConfirm = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiReceiveFavorite = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiConnect = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiChattingMessage = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiAppeal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UseNotiMarketing = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    RecommandCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Referrer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdminPw = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UseUpdateAdminId = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ManagerEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UseTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrivacyTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SensitiveTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarketingTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatentTerm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChattingRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OpenMemberId = table.Column<int>(type: "int", nullable: true),
                    UsePoint = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IsCloseMember1 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsCloseMember2 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StarPoint = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Member1Id = table.Column<int>(type: "int", nullable: true),
                    Member2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChattingRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChattingRoom_Member_Member1Id",
                        column: x => x.Member1Id,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChattingRoom_Member_Member2Id",
                        column: x => x.Member2Id,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    KakaoKey = table.Column<long>(type: "bigint", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Passwd = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Account_Member_Id",
                        column: x => x.Id,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_AlertProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_AlertProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_AlertProfile_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_AlertProfile_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Appraisal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppraisalType = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Appraisal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Appraisal_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_Appraisal_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_BlockPartner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_BlockPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_BlockPartner_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_BlockPartner_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_CharmingPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_CharmingPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_CharmingPoint_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_ChoicePartner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsConfirm = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsSkip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_ChoicePartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_ChoicePartner_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_ChoicePartner_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Hotstrawberry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Hotstrawberry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Hotstrawberry_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Interest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Interest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Interest_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsShow = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Notification_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_NotShowMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_NotShowMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_NotShowMember_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_NotShowMember_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_PointLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcceptPoint = table.Column<int>(type: "int", nullable: false),
                    CurrentPoint = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_PointLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_PointLog_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_Preference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MaxAge = table.Column<int>(type: "int", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    MinTall = table.Column<int>(type: "int", nullable: false),
                    MaxTall = table.Column<int>(type: "int", nullable: false),
                    BeautyOrWealth = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Body = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Religion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alcohol = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Smoking = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_Preference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Preference_Member_Id",
                        column: x => x.Id,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_ProfileImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ratio = table.Column<double>(type: "double", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_ProfileImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_ProfileImage_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_PurchaseLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PurchaseType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Platform = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PurchaseUTCTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsExpire = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_PurchaseLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_PurchaseLog_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_RequestMemberLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsComplete = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_RequestMemberLevel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_RequestMemberLevel_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_RequestRoyal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsFastWork = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsComplete = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_RequestRoyal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_RequestRoyal_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_StarPoint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StarPoint = table.Column<int>(type: "int", nullable: false),
                    IsSkip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_StarPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_StarPoint_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_StarPoint_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_ViewProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_ViewProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_ViewProfile_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_ViewProfile_Member_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PoomPoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Area = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UseComment = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsConfirm = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoomPoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChattingMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsShow = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChattingRoomId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChattingMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChattingMessage_ChattingRoom_ChattingRoomId",
                        column: x => x.ChattingRoomId,
                        principalTable: "ChattingRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChattingMessage_Member_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ChattingMessage_Member_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_AlertPoomPoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PoomPoomId = table.Column<int>(type: "int", nullable: true),
                    AlertMemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_AlertPoomPoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_AlertPoomPoom_Member_AlertMemberId",
                        column: x => x.AlertMemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Member_AlertPoomPoom_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_AlertPoomPoom_PoomPoom_PoomPoomId",
                        column: x => x.PoomPoomId,
                        principalTable: "PoomPoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PoomPoom_Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PoomPoomId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    ReplyMemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoomPoom_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Comment_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Comment_Member_ReplyMemberId",
                        column: x => x.ReplyMemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Comment_PoomPoom_PoomPoomId",
                        column: x => x.PoomPoomId,
                        principalTable: "PoomPoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PoomPoom_Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PoomPoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoomPoom_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Image_PoomPoom_PoomPoomId",
                        column: x => x.PoomPoomId,
                        principalTable: "PoomPoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PoomPoom_Like",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PoomPoomId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoomPoom_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Like_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PoomPoom_Like_PoomPoom_PoomPoomId",
                        column: x => x.PoomPoomId,
                        principalTable: "PoomPoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Member_AlertComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    AlertMemberId = table.Column<int>(type: "int", nullable: true),
                    AlertCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member_AlertComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_AlertComment_Member_AlertMemberId",
                        column: x => x.AlertMemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Member_AlertComment_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Member_AlertComment_PoomPoom_Comment_AlertCommentId",
                        column: x => x.AlertCommentId,
                        principalTable: "PoomPoom_Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChattingMessage_ChattingRoomId",
                table: "ChattingMessage",
                column: "ChattingRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChattingMessage_ReceiverId",
                table: "ChattingMessage",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChattingMessage_SenderId",
                table: "ChattingMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChattingRoom_Member1Id",
                table: "ChattingRoom",
                column: "Member1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChattingRoom_Member2Id",
                table: "ChattingRoom",
                column: "Member2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_Passwd",
                table: "Manager",
                column: "Passwd");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserId",
                table: "Manager",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ApiKey",
                table: "Member",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_Gender",
                table: "Member",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Member_RecommandCode",
                table: "Member",
                column: "RecommandCode");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Account_Email",
                table: "Member_Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_Account_KakaoKey",
                table: "Member_Account",
                column: "KakaoKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertComment_AlertCommentId",
                table: "Member_AlertComment",
                column: "AlertCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertComment_AlertMemberId",
                table: "Member_AlertComment",
                column: "AlertMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertComment_MemberId",
                table: "Member_AlertComment",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertPoomPoom_AlertMemberId",
                table: "Member_AlertPoomPoom",
                column: "AlertMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertPoomPoom_MemberId",
                table: "Member_AlertPoomPoom",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertPoomPoom_PoomPoomId",
                table: "Member_AlertPoomPoom",
                column: "PoomPoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertProfile_MemberId",
                table: "Member_AlertProfile",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_AlertProfile_PartnerId",
                table: "Member_AlertProfile",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Appraisal_AppraisalType",
                table: "Member_Appraisal",
                column: "AppraisalType");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Appraisal_MemberId",
                table: "Member_Appraisal",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Appraisal_PartnerId",
                table: "Member_Appraisal",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_BlockPartner_MemberId",
                table: "Member_BlockPartner",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_BlockPartner_PartnerId",
                table: "Member_BlockPartner",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CharmingPoint_MemberId",
                table: "Member_CharmingPoint",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CharmingPoint_Name",
                table: "Member_CharmingPoint",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ChoicePartner_MemberId",
                table: "Member_ChoicePartner",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ChoicePartner_PartnerId",
                table: "Member_ChoicePartner",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Hotstrawberry_MemberId",
                table: "Member_Hotstrawberry",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Interest_MemberId",
                table: "Member_Interest",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Interest_Name",
                table: "Member_Interest",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Notification_MemberId",
                table: "Member_Notification",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_NotShowMember_MemberId",
                table: "Member_NotShowMember",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_NotShowMember_PartnerId",
                table: "Member_NotShowMember",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_PointLog_MemberId",
                table: "Member_PointLog",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ProfileImage_MemberId",
                table: "Member_ProfileImage",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_PurchaseLog_MemberId",
                table: "Member_PurchaseLog",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_PurchaseLog_Platform",
                table: "Member_PurchaseLog",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Member_PurchaseLog_PurchaseId",
                table: "Member_PurchaseLog",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_RequestMemberLevel_MemberId",
                table: "Member_RequestMemberLevel",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_RequestRoyal_MemberId",
                table: "Member_RequestRoyal",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_StarPoint_MemberId",
                table: "Member_StarPoint",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_StarPoint_PartnerId",
                table: "Member_StarPoint",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ViewProfile_MemberId",
                table: "Member_ViewProfile",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_ViewProfile_PartnerId",
                table: "Member_ViewProfile",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_ContentType",
                table: "PoomPoom",
                column: "ContentType");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_MemberId",
                table: "PoomPoom",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Comment_MemberId",
                table: "PoomPoom_Comment",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Comment_PoomPoomId",
                table: "PoomPoom_Comment",
                column: "PoomPoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Comment_ReplyMemberId",
                table: "PoomPoom_Comment",
                column: "ReplyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Image_PoomPoomId",
                table: "PoomPoom_Image",
                column: "PoomPoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Like_MemberId",
                table: "PoomPoom_Like",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PoomPoom_Like_PoomPoomId",
                table: "PoomPoom_Like",
                column: "PoomPoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChattingMessage");

            migrationBuilder.DropTable(
                name: "HelpMessage");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Member_Account");

            migrationBuilder.DropTable(
                name: "Member_AlertComment");

            migrationBuilder.DropTable(
                name: "Member_AlertPoomPoom");

            migrationBuilder.DropTable(
                name: "Member_AlertProfile");

            migrationBuilder.DropTable(
                name: "Member_Appraisal");

            migrationBuilder.DropTable(
                name: "Member_BlockPartner");

            migrationBuilder.DropTable(
                name: "Member_CharmingPoint");

            migrationBuilder.DropTable(
                name: "Member_ChoicePartner");

            migrationBuilder.DropTable(
                name: "Member_Hotstrawberry");

            migrationBuilder.DropTable(
                name: "Member_Interest");

            migrationBuilder.DropTable(
                name: "Member_Notification");

            migrationBuilder.DropTable(
                name: "Member_NotShowMember");

            migrationBuilder.DropTable(
                name: "Member_PointLog");

            migrationBuilder.DropTable(
                name: "Member_Preference");

            migrationBuilder.DropTable(
                name: "Member_ProfileImage");

            migrationBuilder.DropTable(
                name: "Member_PurchaseLog");

            migrationBuilder.DropTable(
                name: "Member_RequestMemberLevel");

            migrationBuilder.DropTable(
                name: "Member_RequestRoyal");

            migrationBuilder.DropTable(
                name: "Member_StarPoint");

            migrationBuilder.DropTable(
                name: "Member_ViewProfile");

            migrationBuilder.DropTable(
                name: "PoomPoom_Image");

            migrationBuilder.DropTable(
                name: "PoomPoom_Like");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ChattingRoom");

            migrationBuilder.DropTable(
                name: "PoomPoom_Comment");

            migrationBuilder.DropTable(
                name: "PoomPoom");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
