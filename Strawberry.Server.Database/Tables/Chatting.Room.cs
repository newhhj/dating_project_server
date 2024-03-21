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
    [Table(nameof(ChattingRoom))]
    public class ChattingRoom
    {
        [Key]
        public int Id { get; set; }

        public int? OpenMemberId { get; set; }

        public bool UsePoint { get; set; }

        public bool IsCloseMember1 { get; set; }

        public bool IsCloseMember2 { get; set; }

        public int StarPoint { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(Member1))]
        public int? Member1Id { get; set; }
        public Member Member1 { get; set; }

        [ForeignKey(nameof(Member2))]
        public int? Member2Id { get; set; }
        public Member Member2 { get; set; }



        public List<ChattingMessage> ChattingMessages { get; set; }



        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChattingRoom>(table =>
            {
                table.HasOne(x => x.Member1)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.HasOne(x => x.Member2)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.Property(x => x.UsePoint)
                     .HasDefaultValue(false);
            });
        }
    }
}
