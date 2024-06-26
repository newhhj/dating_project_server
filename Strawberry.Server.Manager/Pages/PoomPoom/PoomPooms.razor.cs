﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Manager.Pages.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.PoomPoom
{
    public partial class PoomPooms : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }

        public bool IsInit { get; private set; } = false;
        public PoomPoomsItem[] Items { get; private set; }
        public int Page { get; private set; }
        public int TotalPageCount { get; private set; }
        public ContentTypes? ContentType { get; private set; }
        public string Search { get; private set; }
        public bool IsDispose { get; private set; }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.Navigation.LocationChanged += this.Navigation_LocationChanged;
                await this.GetPageDataAsync();
                this.IsInit = true;
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private async void Navigation_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (this.IsDispose)
                return;

            await this.GetPageDataAsync();
            this.StateHasChanged();
        }

        private void MovePage()
        {
            var uri = new Uri(this.Navigation.Uri);
            var querys = new Dictionary<string, string>()
            {
                { "page", this.Page.ToString() },
                { "contenttype", this.ContentType?.ToString() },
                { "search", this.Search },
            };
            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private async Task GetPageDataAsync()
        {
            var queryText = new Uri(this.Navigation.Uri).Query;
            if (!string.IsNullOrWhiteSpace(queryText))
            {
                var querys = QueryHelpers.ParseQuery(queryText);
                if (querys.ContainsKey("page"))
                {
                    this.Page = int.Parse(querys["page"]);
                }
                if (querys.ContainsKey("contenttype"))
                {
                    this.ContentType = Enum.Parse<ContentTypes>(querys["contenttype"]);
                }
                if (querys.ContainsKey("search"))
                {
                    this.Search = querys["search"];
                }
            }

            var takeCount = 15;

            var query = this.Db.PoomPooms
                .Include(x => x.Member)
                .Include(x => x.PoomPoom_Comments)
                .Include(x => x.PoomPoom_Likes)
                .AsQueryable();

            if (this.ContentType.HasValue)
            {
                query = query
                    .Where(x => x.ContentType == ContentType.Value);
            }
            if (!string.IsNullOrWhiteSpace(this.Search))
            {
                query = query
                    .Where(x => x.Member.Nickname.Contains(this.Search));
            }

            this.TotalPageCount = (int)Math.Ceiling(await query.CountAsync() / (double)takeCount);
            this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

            this.Items = await query
                .Select(x => new PoomPoomsItem
                {
                    ItemId = x.Id,
                    ContentType = x.ContentType,
                    Content = x.Content,
                    Nickname = x.Member.Nickname,
                    CommentCount = x.PoomPoom_Comments.Count,
                    LikeCount = x.PoomPoom_Likes.Count,
                    IsConfirm = x.IsConfirm,
                    CreateTime = x.CreateTime,
                })
                .OrderByDescending(x => x.CreateTime)
                .Skip((this.Page - 1) * takeCount)
                .Take(takeCount)
                .ToArrayAsync();
        }

        private void OpenDetail()
        {
            this.Js.InvokeVoidAsync("open", $"/poompoomdetail", "poompoomdetail", "left=0,top=0,width=500,height=720");
        }

        private void OpenDetail(PoomPoomsItem item)
        {
            this.Js.InvokeVoidAsync("open", $"/poompoomdetail/{item.ItemId}", "poompoomdetail", "left=0,top=0,width=500,height=720");
        }

        private async Task RemoveItemAsync(PoomPoomsItem item)
        {
            if (!await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 삭제하시겠습니까?"))
                return;

            this.Db.PoomPooms.Remove(new Database.Tables.PoomPoom
            {
                Id = item.ItemId
            });
            await this.Db.SaveChangesAsync();
            await this.Js.InvokeVoidAsync("pageReload");
        }

        public void Dispose()
        {
            this.Navigation.LocationChanged -= this.Navigation_LocationChanged;
            this.IsDispose = true;
        }
    }
}
