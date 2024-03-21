using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.PoomPoom
{
    public class PoomPoomsItem
    {
        public int ItemId { get; internal set; }
        public string Nickname { get; internal set; }
        public ContentTypes ContentType { get; internal set; }
        public string Content { get; internal set; }
        public int CommentCount { get; internal set; }
        public int LikeCount { get; internal set; }
        public bool IsConfirm { get; internal set; }
        public DateTime CreateTime { get; internal set; }
    }
}
