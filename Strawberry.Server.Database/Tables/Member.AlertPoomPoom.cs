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
    [Table(nameof(Member_AlertPoomPoom))]
    public class Member_AlertPoomPoom
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey(nameof(PoomPoom))]
        public int? PoomPoomId { get; set; }
        public PoomPoom PoomPoom { get; set; }

        [ForeignKey(nameof(AlertMember))]
        public int? AlertMemberId { get; set; }
        public Member AlertMember { get; set; }



        public static void Modeling(ModelBuilder builder)
        {
            builder.Entity<Member_AlertPoomPoom>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany(x => x.Member_AlertPoomPooms)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.PoomPoom)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.HasOne(x => x.AlertMember)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
