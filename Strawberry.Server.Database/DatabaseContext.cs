using Microsoft.EntityFrameworkCore;
using Strawberry.Server.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database
{
    public class DatabaseContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4", true);

            modelBuilder.HasDbFunction(typeof(DatabaseContext)
                        .GetMethod(nameof(Rand)))
                        .HasName("rand");

            Member.Modeling(modelBuilder);
            Member_Account.Modeling(modelBuilder);
            Member_ChoicePartner.Modeling(modelBuilder);
            Member_AlertProfile.Modeling(modelBuilder);
            Member_AlertPoomPoom.Modeling(modelBuilder);
            Member_AlertComment.Modeling(modelBuilder);
            Member_BlockPartner.Modeling(modelBuilder);
            Member_NotShowMember.Modeling(modelBuilder);
            Member_StarPoint.Modeling(modelBuilder);
            Member_ViewProfile.Modeling(modelBuilder);
            Member_Appraisal.Modeling(modelBuilder);
            Member_RequestMemberLevel.Modeling(modelBuilder);
            Member_PointLog.Modeling(modelBuilder);
            Member_PurchaseLog.Modeling(modelBuilder);
            Member_RequestRoyal.Modeling(modelBuilder);
            Member_Notification.Modeling(modelBuilder);
            Member_ConfirmImage.Modeling(modelBuilder);
            PoomPoom_Comment.Modeling(modelBuilder);
            PoomPoom_Like.Modeling(modelBuilder);
            ChattingRoom.Modeling(modelBuilder);
            ChattingMessage.Modeling(modelBuilder);
            Manager.Modeling(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public void Initialize()
        {
            var canConnect = this.Database.CanConnect();
            this.Database.Migrate();
            if (!canConnect)
            {
                this.Add(new Setting
                {
                    AdminId = "administrator",
                    AdminPw = "0000",
                    UseUpdateAdminId = false,
                    UseTerm = "준비중입니다.",
                    LocationTerm = "준비중입니다.",
                    PrivacyTerm = "준비중입니다.",
                    SensitiveTerm = "준비중입니다.",
                    MarketingTerm = "준비중입니다.",
                });
                this.SaveChanges();
            }
        }


        public DbSet<Setting> Settings { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Member_Account> Member_Accounts { get; set; }
        public DbSet<Member_ProfileImage> Member_ProfileImages { get; set; }
        public DbSet<Member_Preference> Member_Preferences { get; set; }
        public DbSet<Member_CharmingPoint> Member_CharmingPoints { get; set; }
        public DbSet<Member_Interest> Member_Interests { get; set; }
        public DbSet<Member_PurchaseLog> Member_PurchaseLogs { get; set; }
        public DbSet<Member_NotShowMember> Member_NotShowMembers { get; set; }
        public DbSet<Member_Hotstrawberry> Member_Hotstrawberrys { get; set; }
        public DbSet<Member_ChoicePartner> Member_ChoicePartners { get; set; }
        public DbSet<Member_StarPoint> Member_StarPoints { get; set; }
        public DbSet<Member_ViewProfile> Member_ViewProfiles { get; set; }
        public DbSet<Member_AlertProfile> Member_AlertProfiles { get; set; }
        public DbSet<Member_AlertPoomPoom> Member_AlertPoomPooms { get; set; }
        public DbSet<Member_AlertComment> Member_AlertComments { get; set; }
        public DbSet<Member_BlockPartner> Member_BlockPartners { get; set; }
        public DbSet<Member_Notification> Member_Notifications { get; set; }
        public DbSet<Member_ConfirmImage> Member_ConfirmImages { get; set; }
        public DbSet<PoomPoom> PoomPooms { get; set; }
        public DbSet<PoomPoom_Image> PoomPoom_Images { get; set; }
        public DbSet<PoomPoom_Like> PoomPoom_Likes { get; set; }
        public DbSet<PoomPoom_Comment> PoomPoom_Comments { get; set; }
        public DbSet<ChattingRoom> ChattingRooms { get; set; }
        public DbSet<ChattingMessage> ChattingMessages { get; set; }
        public DbSet<Member_Appraisal> Member_Appraisals { get; set; }
        public DbSet<Member_RequestMemberLevel> Member_RequestMemberLevels { get; set; }
        public DbSet<Member_PointLog> Member_PointLogs { get; set; }
        public DbSet<Member_RequestRoyal> Member_RequestRoyals { get; set; }
        public DbSet<HelpMessage> HelpMessages { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<LoveFortune> LoveFortunes { get; set; }
        public DbSet<BlockWord> BlockWords { get; set; }
        public DbSet<ADData> ADDatas { get; set; }

        public double Rand() => throw new NotSupportedException();
    }
}
