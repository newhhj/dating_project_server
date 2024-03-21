using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    public class CallersQueryItem
    {
        public int Page { get; set; } = 1;
        public string Search { get; set; }
        public int TotalPageCount { get; set; } = 1;
    }
}
