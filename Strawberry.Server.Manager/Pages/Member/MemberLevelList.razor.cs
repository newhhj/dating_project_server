using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Manager.Pages.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    [Authorize(Roles = "Administrator")]
    public partial class MemberLevelList : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }


        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;


        private int TotalPageCount { get; set; } = 1;
        private int Page { get; set; } = 1;


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
                { "page", this.Page.ToString() }
            };
            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private void CheckQuerys()
        {
            var queryText = new Uri(this.Navigation.Uri).Query;
            if (string.IsNullOrWhiteSpace(queryText))
                return;

            var querys = QueryHelpers.ParseQuery(queryText);
            if (querys.ContainsKey("page"))
            {
                this.Page = Convert.ToInt32(querys["page"]);
            }
        }

        private void GetDatas()
        {
            var pageItemCount = 15;

            var query = this.Db.Member_RequestMemberLevels
                .Where(x => !string.IsNullOrWhiteSpace(x.Member.RecommandCode));

            this.TotalPageCount = (int)Math.Ceiling(query.Count() / (double)pageItemCount);
            this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

            this.Items = query
                .Select(x => new MembersItem
                {
                    Id = x.Member.Id,
                    Nickname = x.Member.Nickname,
                    Gender = x.Member.Gender,
                    LevelType = x.Member.LevelType,
                    CreateTime = x.Member.CreateTime,
                    MemberState = x.Member.MemberState,
                    IsRoyal = x.Member.IsRoyal,
                    IsVIP = x.Member.IsVIP
                })
                .OrderByDescending(x => x.CreateTime)
                .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                .Take(pageItemCount)
                .ToArray();
        }

        private void OpenDetail(MembersItem item)
        {
            this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.Id}", "detailmember", "left=0,top=0,width=500,height=720");
        }

        private void CompleteItem(MembersItem item)
        {
            var items = this.Db.Member_RequestMemberLevels
                .Where(x => x.MemberId == item.Id)
                .ToArray();

            if (items != null && items.Length > 0)
            {
                this.Db.Member_RequestMemberLevels.RemoveRange(items);
                this.Db.SaveChanges();
            }

            this.GetDatas();
            this.StateHasChanged();
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
