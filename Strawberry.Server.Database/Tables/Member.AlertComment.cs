using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Member_AlertComment))]
    public class Member_AlertComment
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }


        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }


        [ForeignKey(nameof(AlertMember))]
        public int? AlertMemberId { get; set; }
        public Member AlertMember { get; set; }


        [ForeignKey(nameof(AlertComment))]
        public int? AlertCommentId { get; set; }
        public PoomPoom_Comment AlertComment { get; set; }

        public static void Modeling(ModelBuilder builder)
        {
            builder.Entity<Member_AlertComment>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany(x => x.Member_AlertComments)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.AlertMember)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.HasOne(x => x.AlertComment)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}