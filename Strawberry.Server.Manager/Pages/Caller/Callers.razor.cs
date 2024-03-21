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

namespace Strawberry.Server.Manager.Pages.Caller
{
    public partial class Callers
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }

        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;
        private CallersQueryItem QueryItem { get; set; }
        private CallersItem[] Items { get; set; }

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
            };
            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private void CheckQuerys()
        {
            this.QueryItem = new CallersQueryItem();

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
        }

        private void GetDatas()
        {
            var pageItemCount = 15;

            var query = this.Db.Members
                .Where(x => string.IsNullOrWhiteSpace(x.RecommandCode));

            if (!string.IsNullOrWhiteSpace(this.QueryItem.Search))
            {
                query = query
                    .Where(x => x.Nickname.Contains(this.QueryItem.Search)
                             || x.Member_Account.Email.Contains(this.QueryItem.Search));
            }

            this.QueryItem.TotalPageCount = (int)Math.Ceiling(query.Count() / (double)pageItemCount);
            this.QueryItem.Page = Math.Min(this.QueryItem.TotalPageCount, Math.Max(1, this.QueryItem.Page));

            this.Items = query
                .Select(x => new CallersItem
                {
                    Id = x.Id,
                    Nickname = x.Nickname,
                    Gender = x.Gender,
                    LevelType = x.LevelType,
                    CreateTime = x.CreateTime
                })
                .OrderByDescending(x => x.CreateTime)
                .Skip((Math.Max(1, this.QueryItem.Page) - 1) * pageItemCount)
                .Take(pageItemCount)
                .ToArray();
        }

        private void OpenDetail()
        {
            this.Js.InvokeVoidAsync("open", $"/callersdetail", "detailcaller", "left=0,top=0,width=500,height=720");
        }

        private void OpenDetail(CallersItem item)
        {
            this.Js.InvokeVoidAsync("open", $"/callersdetail/{item.Id}", "detailcaller", "left=0,top=0,width=500,height=720");
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
