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
    [Table(nameof(Member_PurchaseLog))]
    [Index(nameof(Platform))]
    [Index(nameof(PurchaseId))]
    public class Member_PurchaseLog
    {
        [Key]
        public int Id { get; set; }

        public PurchaseTypes PurchaseType { get; set; }

        public string Platform { get; set; }

        public string ProductId { get; set; }

        public string PurchaseId { get; set; }

        public string PurchaseToken { get; set; }

        public DateTime PurchaseUTCTime { get; set; }

        public bool IsExpire { get; set; }

        public DateTime? ExpireTime { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }



        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_PurchaseLog>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Cascade);

                table.Property(x => x.PurchaseType)
                     .HasDefaultValue(PurchaseTypes.Product);
                table.Property(x => x.IsExpire)
                     .HasDefaultValue(false);
            });
        }
    }
}
