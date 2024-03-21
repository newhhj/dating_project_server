using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    public partial class Members : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }

        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;
        private MembersQueryItem QueryItem { get; set; }
        private MembersItem[] Items { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                this.Navigation.LocationChanged += this.Navigation_LocationChanged;
                this.IsInit = true;
                this.CheckQuerys();
                this.GetDatas();
                this.StateHasChanged();
            }
        }

        private void Navigation_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (this.IsDispose)
                return;

            this.CheckQuerys();
            this.GetDatas();
            this.StateHasChanged();
        }

        private void MovePage()
        {
            var uri = new Uri(this.Navigation.Uri);
            var querys = new Dictionary<string, string>()
            {
                { "page", this.QueryItem.Page.ToString() },
                { "search", this.QueryItem.Search },
                { "gender", this.QueryItem.Gender?.ToString() },
                { "leveltype", this.QueryItem.LevelType?.ToString() },
                { "memberstate", this.QueryItem.MemberState?.ToString() },
                { "etctype", this.QueryItem.EtcType?.ToString() },
            };
            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private void CheckQuerys()
        {
            this.QueryItem = new MembersQueryItem();

            var queryText = new Uri(this.Navigation.Uri).Query;
            if (string.IsNullOrWhiteSpace(queryText))
                return;

            var querys = QueryHelpers.ParseQuery(queryText);
            if (querys.ContainsKey("page"))
            {
                this.QueryItem.Page = Convert.ToInt32(querys["page"]);
            }
            if (querys.ContainsKey("search"))
            {
                this.QueryItem.Search = Convert.ToString(querys["search"]);
            }
            if (querys.ContainsKey("gender"))
            {
                var data = (string)querys["gender"];
                if (!string.IsNullOrWhiteSpace(data))
                    this.QueryItem.Gender = int.Parse(data);
            }
            if (querys.ContainsKey("leveltype"))
            {
                var data = (string)querys["leveltype"];
                if (!string.IsNullOrWhiteSpace(data))
                    this.QueryItem.LevelType = int.Parse(data);
            }
            if (querys.ContainsKey("memberstate"))
            {
                var data = (string)querys["memberstate"];
                if (!string.IsNullOrWhiteSpace(data))
                    this.QueryItem.MemberState = int.Parse(data);
            }
            if (querys.ContainsKey("etctype"))
            {
                var data = (string)querys["etctype"];
                if (!string.IsNullOrWhiteSpace(data))
                    this.QueryItem.EtcType = data;
            }
        }

        private void GetDatas()
        {
            var pageItemCount = 15;

            var query = this.Db.Members
                .Where(x => !string.IsNullOrWhiteSpace(x.RecommandCode));

            if (this.QueryItem.Gender.HasValue)
            {
                query = query
                    .Where(x => x.Gender == (GenderTypes)this.QueryItem.Gender.Value);
            }

            if (this.QueryItem.LevelType.HasValue)
            {
                query = query
                    .Where(x => x.LevelType == (LevelTypes)this.QueryItem.LevelType.Value);
            }

            if (this.QueryItem.MemberState.HasValue)
            {
                query = query
                    .Where(x => x.MemberState == (MemberStateTypes)this.QueryItem.MemberState.Value);
            }

            if (!string.IsNullOrWhiteSpace(this.QueryItem.EtcType))
            {
                if (this.QueryItem.EtcType == "10")
                {
                    query = query
                        .Where(x => x.HasStarBadge);
                }
                else if (this.QueryItem.EtcType == "VIP")
                {
                    query = query
                        .Where(x => x.IsVIP);
                }
                else
                {
                    query = query
                        .Where(x => x.IsRoyal);
                }
            }

            if (!string.IsNullOrWhiteSpace(this.QueryItem.Search))
            {
                query = query
                    .Where(x => x.Nickname.Contains(this.QueryItem.Search)
                             || x.Member_Account.Email.Contains(this.QueryItem.Search));
            }

            this.QueryItem.TotalPageCount = (int)Math.Ceiling(query.Count() / (double)pageItemCount);
            this.QueryItem.Page = Math.Min(this.QueryItem.TotalPageCount, Math.Max(1, this.QueryItem.Page));

            this.Items = query
                .Select(x => new MembersItem
                {
                    Id = x.Id,
                    Nickname = x.Nickname,
                    Gender = x.Gender,
                    LevelType = x.LevelType,
                    CreateTime = x.CreateTime,
                    MemberState = x.MemberState,
                    IsRoyal = x.IsRoyal,
                    IsVIP = x.IsVIP
                })
                .OrderByDescending(x => x.CreateTime)
                .Skip((Math.Max(1, this.QueryItem.Page) - 1) * pageItemCount)
                .Take(pageItemCount)
                .ToArray();
        }

        private void OpenDetail(MembersItem item)
        {
            this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.Id}", "detailmember", "left=0,top=0,width=500,height=720");
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
