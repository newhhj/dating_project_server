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
    [Table(nameof(PoomPoom))]
    [Index(nameof(ContentType))]
    public class PoomPoom
    {
        [Key]
        public int Id { get; set; }

        public ContentTypes ContentType { get; set; }

        public string Content { get; set; }

        public string Area { get; set; }

        public string Time { get; set; }

        public bool UseComment { get; set; }

        public bool IsConfirm { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }



        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }



        public List<PoomPoom_Image> PoomPoom_Images { get; set; }
        public List<PoomPoom_Like> PoomPoom_Likes { get; set; }
        public List<PoomPoom_Comment> PoomPoom_Comments { get; set; }
    }
}
