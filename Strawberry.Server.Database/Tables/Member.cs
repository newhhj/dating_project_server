using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Member))]
    [Index(nameof(ApiKey), IsUnique = true)]
    [Index(nameof(Gender))]
    [Index(nameof(RecommandCode))]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApiKey { get; set; }

        public MemberStateTypes MemberState { get; set; }

        public DateTime? TermCheckTime { get; set; }

        public DateTime? PrivacyCheckTime { get; set; }

        public DateTime? LocationCheckTime { get; set; }

        public DateTime? SensitiveCheckTime { get; set; }

        public DateTime? MarketingCheckTime { get; set; }

        public GenderTypes Gender { get; set; }

        public DateTime BirthDay { get; set; }

        public string Nickname { get; set; }

        public int Tall { get; set; }

        /// <summary>
        /// 체형
        /// </summary>
        public string BodyStyle { get; set; }
        
        /// <summary>
        /// 학력
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 학교명
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 직업
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 직장이름
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 종교
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// 음주
        /// </summary>
        public string Alcohol { get; set; }

        /// <summary>
        /// 흡연
        /// </summary>
        public string Smoking { get; set; }

        /// <summary>
        /// 성격
        /// </summary>
        public string Personality { get; set; }

        /// <summary>
        /// 혈액형
        /// </summary>
        public string Blood { get; set; }

        /// <summary>
        /// 위도
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 경도
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// 회원 등급
        /// </summary>
        public LevelTypes LevelType { get; set; }

        /// <summary>
        /// 회원 등급 점수
        /// </summary>
        public double LevelPoint { get; set; }

        /// <summary>
        /// 로얄회원여부
        /// </summary>
        public bool IsRoyal { get; set; }

        /// <summary>
        /// 10스타 배지
        /// </summary>
        public bool HasStarBadge { get; set; }

        /// <summary>
        /// 무료 초이스 횟수
        /// </summary>
        public int FreeChoiceCount { get; set; }

        /// <summary>
        /// 무료 채팅 횟수
        /// </summary>
        public int FreeChattingCount { get; set; }

        /// <summary>
        /// VIP회원 여부
        /// </summary>
        public bool IsVIP { get; set; }

        /// <summary>
        /// 추가 3초이스 만료일
        /// </summary>
        public DateTime? AddChoice3Time { get; set; }

        /// <summary>
        /// 추가 1대화 만료일
        /// </summary>
        public DateTime? AddChatting1Time { get; set; }

        /// <summary>
        /// 추가 3대화 만료일
        /// </summary>
        public DateTime? AddChatting3Time { get; set; }

        /// <summary>
        /// 무제한 초이스 만료일
        /// </summary>
        public DateTime? FreeChoiceTime { get; set; }

        /// <summary>
        /// 무제한 스마트 초이스 만료일
        /// </summary>
        public DateTime? FreeSmartChoiceTime { get; set; }

        /// <summary>
        /// 무제한 대화 만료일
        /// </summary>
        public DateTime? FreeChattingTime { get; set; }

        /// <summary>
        /// 첫인사 메세지
        /// </summary>
        public string FirstMessage { get; set; }

        /// <summary>
        /// 첫인사 음성
        /// </summary>
        public string FirstVoice { get; set; }

        /// <summary>
        /// 운영체제
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 푸시토큰
        /// </summary>
        public string PushToken { get; set; }

        /// <summary>
        /// 마지막 로그인 시간
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 보유 포인트(딸기)
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 매력율
        /// </summary>
        public int RateCharming { get; set; }

        /// <summary>
        /// 응답율
        /// </summary>
        public int RateResponse { get; set; }

        public bool UseNotiRecommand { get; set; }

        public bool UseNotiReceiveChoice { get; set; }

        public bool UseNotiSendChoiceConfirm { get; set; }

        public bool UseNotiReceiveFavorite { get; set; }

        public bool UseNotiConnect { get; set; }

        public bool UseNotiChattingMessage { get; set; }

        public bool UseNotiAppeal { get; set; }

        public bool UseNotiMarketing { get; set; }

        [MaxLength(100)]
        public string RecommandCode { get; set; }

        public string Referrer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }







        public Member_Account Member_Account { get; set; }
        public Member_Preference Member_Preference { get; set; }
        public List<Member_ProfileImage> Member_ProfileImages { get; set; }
        public List<Member_CharmingPoint> Member_CharmingPoints { get; set; }
        public List<Member_Interest> Member_Interests { get; set; }
        public List<Member_NotShowMember> Member_NotShowMembers { get; set; }
        public List<Member_Hotstrawberry> Member_Hotstrawberrys { get; set; }
        public List<Member_ChoicePartner> Member_ChoicePartners { get; set; }
        public List<Member_StarPoint> Member_StarPoints { get; set; }
        public List<Member_ViewProfile> Member_ViewProfiles { get; set; }
        public List<Member_AlertProfile> Member_AlertProfiles { get; set; }
        public List<Member_AlertPoomPoom> Member_AlertPoomPooms { get; set; }
        public List<Member_AlertComment> Member_AlertComments { get; set; }
        public List<Member_BlockPartner> Member_BlockPartners { get; set; }
        public List<PoomPoom> PoomPooms { get; set; }

        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>(table =>
            {
                table.Property(x => x.UseNotiRecommand)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiReceiveChoice)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiSendChoiceConfirm)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiReceiveFavorite)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiConnect)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiChattingMessage)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiAppeal)
                     .HasDefaultValue(true);
                table.Property(x => x.UseNotiMarketing)
                     .HasDefaultValue(true);
            });
        }
    }
}
