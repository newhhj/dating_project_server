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
    [Table(nameof(PoomPoom_Like))]
    public class PoomPoom_Like
    {
        [Key]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(PoomPoom))]
        public int PoomPoomId { get; set; }
        public PoomPoom PoomPoom { get; set; }



        [ForeignKey(nameof(Member))]
        public int? MemberId { get; set; }
        public Member Member { get; set; }


        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PoomPoom_Like>(table =>
            {
                table.HasOne(x => x.PoomPoom)
                     .WithMany(x => x.PoomPoom_Likes)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.Member)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
