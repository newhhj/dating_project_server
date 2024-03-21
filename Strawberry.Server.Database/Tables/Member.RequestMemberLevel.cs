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
    [Table(nameof(Member_RequestMemberLevel))]
    public class Member_RequestMemberLevel
    {
        [Key]
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_RequestMemberLevel>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Cascade);

                table.Property(x => x.IsComplete)
                     .HasDefaultValue(false);
            });
        }
    }
}
