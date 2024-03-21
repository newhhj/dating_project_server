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
    [Table(nameof(PoomPoom_Comment))]
    [Index(nameof(MemberId))]
    public class PoomPoom_Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(PoomPoom))]
        public int PoomPoomId { get; set; }
        public PoomPoom PoomPoom { get; set; }



        [ForeignKey(nameof(Member))]
        public int? MemberId { get; set; }
        public Member Member { get; set; }



        [ForeignKey(nameof(ReplyMember))]
        public int? ReplyMemberId { get; set; }
        public Member ReplyMember { get; set; }



        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoomPoom_Comment>(table =>
            {
                table.HasOne(x => x.PoomPoom)
                     .WithMany(x => x.PoomPoom_Comments)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.Member)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.HasOne(x => x.ReplyMember)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
