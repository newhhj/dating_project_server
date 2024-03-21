using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Strawberry.Server.Manager.Pages.Alert
{
    [Authorize(Roles = "Administrator")]
    public partial class AlertList : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }


        private bool IsInit { get; set; } = false;
        private bool IsDispose { get; set; } = false;


        public int ContentType { get; set; } = 0;
        private int TotalPageCount { get; set; } = 1;
        private int Page { get; set; } = 1;


        private dynamic[] Items { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                this.Navigation.LocationChanged += this.Navigation_LocationChanged;
                this.IsInit = true;
                this.GetPageData();
                this.StateHasChanged();
            }
        }

        private void Navigation_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (this.IsDispose)
                return;

            this.GetPageData();
            this.StateHasChanged();
        }

        private void MovePage()
        {
            var uri = new Uri(this.Navigation.Uri);
            var querys = new Dictionary<string, string>()
            {
                { "contenttype", this.ContentType.ToString() },
                { "page", this.Page.ToString() },
            };
            var url = QueryHelpers.AddQueryString(uri.AbsolutePath, querys);
            this.Navigation.NavigateTo(url);
        }

        private void ChangeContentType(int contentType)
        {
            this.ContentType = contentType;
            this.Page = 1;
            this.MovePage();
        }

        private void GetPageData()
        {
            var queryText = new Uri(this.Navigation.Uri).Query;
            if (!string.IsNullOrWhiteSpace(queryText))
            {
                var querys = QueryHelpers.ParseQuery(queryText);
                if (querys.ContainsKey("page"))
                {
                    this.Page = Convert.ToInt32(querys["page"]);
                }
                if (querys.ContainsKey("contenttype"))
                {
                    this.ContentType = Convert.ToInt32(querys["contenttype"]);
                }
            }

            var pageItemCount = 15;


            switch (this.ContentType)
            {
                case 0:
                {
                    var query = this.Db.Member_AlertProfiles
                        .AsQueryable();

                    this.TotalPageCount = Math.Max(1, (int)Math.Ceiling(query.Count() / (double)pageItemCount));
                    this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

                    this.Items = query
                        .Select(x => new
                        {
                            x.Id,
                            ContentType = 0,
                            x.CreateTime,
                            x.Title
                        })
                        .OrderByDescending(x => x.CreateTime)
                        .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                        .Take(pageItemCount)
                        .ToArray();
                    break;
                }
                case 1:
                {
                    var query = this.Db.Member_AlertPoomPooms
                        .AsQueryable();

                    this.TotalPageCount = Math.Max(1, (int)Math.Ceiling(query.Count() / (double)pageItemCount));
                    this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

                    this.Items = query
                        .Select(x => new
                        {
                            x.Id,
                            ContentType = 1,
                            x.CreateTime,
                            x.Title
                        })
                        .OrderByDescending(x => x.CreateTime)
                        .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                        .Take(pageItemCount)
                        .ToArray();
                    break;
                }
                case 2:
                {
                    var query = this.Db.Member_AlertComments
                        .AsQueryable();

                    this.TotalPageCount = Math.Max(1, (int)Math.Ceiling(query.Count() / (double)pageItemCount));
                    this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

                    this.Items = query
                        .Select(x => new
                        {
                            x.Id,
                            ContentType = 2,
                            x.CreateTime,
                            x.Title
                        })
                        .OrderByDescending(x => x.CreateTime)
                        .Skip((Math.Max(1, this.Page) - 1) * pageItemCount)
                        .Take(pageItemCount)
                        .ToArray();
                    break;
                }
                default:
                    break;
            }
        }

        private void OpenDetail(dynamic item)
        {
            this.Js.InvokeVoidAsync("open", $"/alertdetail/{item.ContentType}/{item.Id}", "alertdetail", "left=0,top=0,width=500,height=720");
        }

        private async Task RemoveItemAsync(dynamic item)
        {
            if (await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 삭제하시겠습니까?"))
            {
                switch (item.ContentType)
                {
                    case 0:
                    {
                        this.Db.Member_AlertProfiles.Remove(new Database.Tables.Member_AlertProfile { Id = item.Id });
                        this.Db.SaveChanges();
                        await this.Js.InvokeVoidAsync("pageReload");
                        break;
                    }
                    case 1:
                    {
                        this.Db.Member_AlertPoomPooms.Remove(new Database.Tables.Member_AlertPoomPoom { Id = item.Id });
                        this.Db.SaveChanges();
                        await this.Js.InvokeVoidAsync("pageReload");
                        break;
                    }
                    case 2:
                    {
                        this.Db.Member_AlertComments.Remove(new Database.Tables.Member_AlertComment { Id = item.Id });
                        this.Db.SaveChanges();
                        await this.Js.InvokeVoidAsync("pageReload");
                        break;
                    }
                    default:
                        break;
                }
            }
        }

        public void Dispose()
        {
            this.IsDispose = true;
            this.Navigation.LocationChanged -= Navigation_LocationChanged;
        }
    }
}
