using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Strawberry.Server.Manager.Pages.RequestRoyal
{
    [Authorize(Roles = "Administrator")]
    public partial class RequestRoyalList : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }


        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;


        private int TotalPageCount { get; set; } = 1;
        private int Page { get; set; } = 1;
        private bool? IsFastWork { get; set; }
        private bool? IsComplete { get; set; }


        private Database.Tables.Member_RequestRoyal[] Items { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                this.Navigation.LocationChanged += this.Navigation_LocationChanged;
                this.IsInit = true;
                this.GetDatas();
                this.StateHasChanged();
            }
        }

        private void Navigation_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (this.IsDispose)
                return;

            this.GetDatas();
            this.StateHasChanged();
        }

        private void MovePage(object page, object isFastWork, object isComplete)
        {
            var uri = new Uri(this.Navigation.Uri);
            var querys = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(page?.ToString() ?? ""))
                querys.Add(nameof(Page), page.ToString());
            if (!string.IsNullOrWhiteSpace(isFastWork?.ToString() ?? ""))
                querys.Add(nameof(IsFastWork), isFastWork.ToString());
            if (!string.IsNullOrWhiteSpace(isComplete?.ToString() ?? ""))
                querys.Add(nameof(IsComplete), isComplete.ToString());

            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private void GetDatas()
        {
            var queryText = new Uri(this.Navigation.Uri).Query;
            if (!string.IsNullOrWhiteSpace(queryText))
            {
                var querys = QueryHelpers.ParseQuery(queryText);
                
                if (querys.ContainsKey(nameof(Page)) && int.TryParse(querys[nameof(Page)], out var page))
                    this.Page = page;
                else
                    this.Page = 1;

                if (querys.ContainsKey(nameof(IsFastWork)) && bool.TryParse(querys[nameof(IsFastWork)], out var isFastWork))
                    this.IsFastWork = isFastWork;
                else
                    this.IsFastWork = null;

                if (querys.ContainsKey(nameof(IsComplete)) && bool.TryParse(querys[nameof(IsComplete)], out var isComplete))
                    this.IsComplete = isComplete;
                else
                    this.IsComplete = null;
            }

            var pageItemCount = 15;

            var query = this.Db.Member_RequestRoyals
                .AsQueryable();

            if (this.IsFastWork.HasValue)
            {
                query = query
                    .Where(x => x.IsFastWork == this.IsFastWork.Value);
            }

            if (this.IsComplete.HasValue)
            {
                query = query
                    .Where(x => x.IsComplete == this.IsComplete.Value);
            }

            this.TotalPageCount = Math.Max(1, (int)Math.Ceiling(query.Count() / (double)pageItemCount));
            this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

            this.Items = query
                .Include(x => x.Member)
                .OrderByDescending(x => x.CreateTime)
                .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                .Take(pageItemCount)
                .ToArray();
        }

        private void OpenDetail(Member_RequestRoyal item)
        {
            this.Js.InvokeVoidAsync("open", $"/requestroyaldetail/{item.Id}", "requestroyaldetail", "left=0,top=0,width=500,height=720");
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
