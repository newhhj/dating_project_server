using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strawberry.Server.Database.Tables
{
    [Table(nameof(Manager))]
    [Index(nameof(UserId))]
    [Index(nameof(Passwd))]
    public class Manager
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string UserId { get; set; }

        [Required, MaxLength(100)]
        public string Passwd { get; set; }
        
        [Required]
        public string Nickname { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }

        [Required]
        public bool Useage { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        public static void Modeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(table =>
            {
                table.Property(x => x.Role)
                     .HasDefaultValue("Manager");

                table.Property(x => x.Useage)
                     .HasDefaultValue(true);
            });
        }
    }
}
