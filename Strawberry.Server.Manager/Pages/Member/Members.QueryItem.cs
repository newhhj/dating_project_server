using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    public class MembersQueryItem
    {
        public int Page { get; set; } = 1;
        public string Search { get; set; }
        public int? Gender { get; set; }
        public int? LevelType { get; set; }
        public int? MemberState { get; set; }
        public int TotalPageCount { get; set; } = 1;
        public string EtcType { get; set; }
    }
}
