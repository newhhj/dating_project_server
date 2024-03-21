using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Member_ProfileImage))]
    public class Member_ProfileImage
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public double Ratio { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
