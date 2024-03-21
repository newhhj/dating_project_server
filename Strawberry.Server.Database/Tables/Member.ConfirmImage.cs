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
    /// <summary>
    /// 승인 대상 이미지
    /// </summary>
    [Table(nameof(Member_ConfirmImage))]
    [Comment("승인 대상 이미지")]
    public class Member_ConfirmImage
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }

        /// <summary>
        /// Foreign Key To Member
        /// </summary>
        [Comment("Foreign Key To Member")]
        public int MemberId { get; set; }

        /// <summary>
        /// 이미지 타입
        /// </summary>
        [Comment("이미지 타입")]
        public ConfirmImageTypes ImageType { get; set; }

        /// <summary>
        /// 주 컨텐츠 아이디
        /// </summary>
        [Comment("주 컨텐츠 아이디")]
        public int? ContentId { get; set; }

        /// <summary>
        /// 이미지 Primary Key
        /// </summary>
        [Comment("이미지 Primary Key")]
        public int? ImageId { get; set; }

        /// <summary>
        /// 이미지 경로
        /// </summary>
        [Comment("이미지 경로")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 회원정보
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// 등록시간
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("등록시간")]
        public DateTime CreateTime { get; set; }

        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_ConfirmImage>(x =>
            {
                x.HasOne(x => x.Member)
                 .WithMany()
                 .HasForeignKey(x => x.MemberId)
                 .HasPrincipalKey(x => x.Id)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
