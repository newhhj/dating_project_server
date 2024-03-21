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
    [Table(nameof(Member_Account))]
    [Index(nameof(KakaoKey), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Member_Account
    {
        [Key]
        [ForeignKey(nameof(Member))]
        public int Id { get; set; }

        public long? KakaoKey { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Passwd { get; set; }


        public Member Member { get; set; }

        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member_Account>(table =>
            {
                table.HasOne(x => x.Member)
                     .WithOne(x => x.Member_Account)
                     .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
