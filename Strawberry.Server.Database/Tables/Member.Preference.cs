using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Member_Preference))]
    public class Member_Preference
    {
        [Key]
        [ForeignKey(nameof(Member))]
        public int Id { get; set; }

        /// <summary>
        /// 최소 선호 나이
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// 최대 선호 나이
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// 선호 거리
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// 최소 선호 키
        /// </summary>
        public int MinTall { get; set; }

        /// <summary>
        /// 최대 선호 키
        /// </summary>
        public int MaxTall { get; set; }

        /// <summary>
        /// 미모 또는 재력
        /// </summary>
        public bool BeautyOrWealth { get; set; }

        public string Body { get; set; }

        public string Religion { get; set; }

        public string Alcohol { get; set; }

        public string Smoking { get; set; }

        /// <summary>
        /// 우선 매칭 조건
        /// </summary>
        public PriorityTypes Priority { get; set; }

        public Member Member { get; set; }
    }
}
