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
    [Table(nameof(Member_Notification))]
    public class Member_Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public bool IsShow { get; set; }

        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }



        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_Notification>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
