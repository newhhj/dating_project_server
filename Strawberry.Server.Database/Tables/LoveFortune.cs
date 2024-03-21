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
    /// 연애기운 정보
    /// </summary>
    [Table(nameof(LoveFortune))]
    [Comment("연애기운 정보")]
    public class LoveFortune
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }

        /// <summary>
        /// 메세지
        /// </summary>
        [Required]
        [Comment("메세지")]
        public string Message { get; set; }

        /// <summary>
        /// 등록시간
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("등록시간")]
        public DateTime CreateTime { get; set; }
    }
}
