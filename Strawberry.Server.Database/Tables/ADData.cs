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
    /// 광고정보 테이블
    /// </summary>
    [Table(nameof(ADData))]
    [Comment("광고정보 테이블")]
    public class ADData
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }

        /// <summary>
        /// 광고 타입
        /// </summary>
        [Comment("광고 타입")]
        public ADTypes ADType { get; set; }

        /// <summary>
        /// 광고명
        /// </summary>
        [Required]
        [Comment("광고명")]
        public string ADName { get; set; }

        /// <summary>
        /// 연결링크
        /// </summary>
        [Comment("연결링크")]
        public string Link { get; set; }

        /// <summary>
        /// 추가 데이터 (Json)
        /// </summary>
        [Comment("추가 데이터 (Json)")]
        public string JsonData { get; set; }

        /// <summary>
        /// 등록시간
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("등록시간")]
        public DateTime CreateTime { get; set; }
    }
}
