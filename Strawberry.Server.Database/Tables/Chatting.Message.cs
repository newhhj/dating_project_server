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
    [Table(nameof(ChattingMessage))]
    public class ChattingMessage
    {
        [Key]
        public int Id { get; set; }

        public MessageTypes Type { get; set; }

        public string Content { get; set; }

        public bool IsShow { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(ChattingRoom))]
        public int ChattingRoomId { get; set; }
        public ChattingRoom ChattingRoom { get; set; }

        [ForeignKey(nameof(Sender))]
        public int? SenderId { get; set; }
        public Member Sender { get; set; }

        [ForeignKey(nameof(Receiver))]
        public int? ReceiverId { get; set; }
        public Member Receiver { get; set; }



        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChattingMessage>(table =>
            {
                table.HasOne(x => x.ChattingRoom)
                     .WithMany(x => x.ChattingMessages)
                     .OnDelete(DeleteBehavior.Cascade);

                table.HasOne(x => x.Sender)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);

                table.HasOne(x => x.Receiver)
                     .WithMany()
                     .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
