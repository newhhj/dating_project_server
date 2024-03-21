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
    [Table(nameof(PoomPoom_Image))]
    public class PoomPoom_Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(PoomPoom))]
        public int PoomPoomId { get; set; }
        public PoomPoom PoomPoom { get; set; }
    }
}
