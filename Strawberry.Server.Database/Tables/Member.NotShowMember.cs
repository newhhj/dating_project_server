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
    [Table(nameof(Member_NotShowMember))]
    public class Member_NotShowMember
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }


        [ForeignKey(nameof(Partner))]
        public int? PartnerId { get; set; }
        public Member Partner { get; set; }


        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_NotShowMember>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany(x => x.Member_NotShowMembers)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.Partner)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
