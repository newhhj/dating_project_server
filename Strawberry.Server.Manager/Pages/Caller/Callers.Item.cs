using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    public class CallersItem
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public GenderTypes Gender { get; set; }
        public LevelTypes LevelType { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
