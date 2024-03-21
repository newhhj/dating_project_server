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
using Microsoft.AspNetCore.Authorization;

namespace Strawberry.Server.Manager.Pages.Manager
{
    [Authorize(Roles = "Administrator")]
    public partial class ManagerList : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }


        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;


        private int TotalPageCount { get; set; } = 1;
        private int Page { get; set; } = 1;


        private Database.Tables.Manager[] Items { get; set; }

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

        private void GetDatas()
        {
            var queryText = new Uri(this.Navigation.Uri).Query;
            if (!string.IsNullOrWhiteSpace(queryText))
            {
                var querys = QueryHelpers.ParseQuery(queryText);
                if (querys.ContainsKey("page"))
                {
                    this.Page = Convert.ToInt32(querys["page"]);
                }
            }

            var pageItemCount = 15;

            var query = this.Db.Managers
                .AsQueryable();

            this.TotalPageCount = (int)Math.Ceiling(query.Count() / (double)pageItemCount);
            this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

            this.Items = query
                .OrderByDescending(x => x.CreateTime)
                .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                .Take(pageItemCount)
                .ToArray();
        }

        private async void RemoveItemAsync(Database.Tables.Manager item)
        {
            if (await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 삭제하시겠습니까?"))
            {
                this.Db.Managers.Remove(item);
                this.Db.SaveChanges();
                await this.Js.InvokeVoidAsync("pageReload");
            }
        }

        private void OpenDetail()
        {
            this.Js.InvokeVoidAsync("open", $"/managerlistitem", "managerlistitem", "left=0,top=0,width=500,height=720");
        }

        private void OpenDetail(Database.Tables.Manager item)
        {
            this.Js.InvokeVoidAsync("open", $"/managerlistitem/{item.Id}", "managerlistitem", "left=0,top=0,width=500,height=720");
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
