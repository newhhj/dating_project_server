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
    /// 제한 단어
    /// </summary>
    [Table(nameof(BlockWord))]
    [Comment("제한 단어")]
    public class BlockWord
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }

        /// <summary>
        /// 단어
        /// </summary>
        [Required]
        [Comment("단어")]
        public string Word { get; set; }

        /// <summary>
        /// 등록시간
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("등록시간")]
        public DateTime CreateTime { get; set; }
    }
}
