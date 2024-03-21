using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Manager.Pages.PoomPoom;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Strawberry.Server.Manager.Helpers;

namespace Strawberry.Server.Manager.Pages.ImageConfirm
{
    public partial class ImageConfirmList : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] FirebaseHelper FirebaseHelper { get; set; }

        public bool IsInit { get; private set; } = false;
        public ImageConfirmItem[] Items { get; private set; }
        public int Page { get; private set; }
        public int TotalPageCount { get; private set; }
        public ConfirmImageTypes? ImageType { get; private set; }
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
                { "imagetype", this.ImageType?.ToString() },
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
                if (querys.ContainsKey("imagetype"))
                {
                    this.ImageType = Enum.Parse<ConfirmImageTypes>(querys["imagetype"]);
                }
            }

            var takeCount = 15;

            var query = this.Db.Member_ConfirmImages
                .Include(x => x.Member)
                .AsQueryable();

            if (this.ImageType.HasValue)
            {
                query = query
                    .Where(x => x.ImageType == ImageType.Value);
            }

            this.TotalPageCount = Math.Max(1, (int)Math.Ceiling(await query.CountAsync() / (double)takeCount));
            this.Page = Math.Min(this.TotalPageCount, Math.Max(1, this.Page));

            this.Items = await query
                .Select(x => new ImageConfirmItem
                {
                    ItemId = x.Id,
                    ImageId = x.ImageId,
                    ContentId = x.ContentId,
                    ImageType = x.ImageType,
                    ImageUrl = x.ImageUrl,
                    MemberId = x.Member.Id,
                    Nickname = x.Member.Nickname,
                    CreateTime = x.CreateTime,
                    PushToken = x.Member.PushToken
                })
                .OrderByDescending(x => x.CreateTime)
                .Skip((this.Page - 1) * takeCount)
                .Take(takeCount)
                .ToArrayAsync();
        }

        private void OpenOwnerDetail(ImageConfirmItem item)
        {
            switch (item.ImageType)
            {
                default:
                case ConfirmImageTypes.ProfileImage:
                    this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.MemberId}", "detailmember", "left=0,top=0,width=500,height=720");
                    break;
                case ConfirmImageTypes.PoomPoomImage:
                    this.Js.InvokeVoidAsync("open", $"/poompoomdetail/{item.ContentId}", "poompoomdetail", "left=0,top=0,width=500,height=720");
                    break;
            }
        }

        private void OpenDetail(ImageConfirmItem item)
        {
            this.Js.InvokeVoidAsync("open", item.ImageUrl, "confirmimage", "left=0,top=0,width=500,height=500");
        }

        private async Task AcceptItemAsync(ImageConfirmItem item)
        {
            if (!await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 승인하시겠습니까?"))
                return;

            switch (item.ImageType)
            {
                default:
                case ConfirmImageTypes.ProfileImage:
                    {
                        if (item.ImageId.HasValue)
                        {
                            var imageItem = this.Db.Member_ProfileImages.FirstOrDefault(x => x.Id == item.ImageId.Value);
                            imageItem.Url = item.ImageUrl;
                        }
                        else
                        {
                            this.Db.Member_ProfileImages.Add(new Database.Tables.Member_ProfileImage
                            {
                                CreateTime = DateTime.Now,
                                MemberId = item.ContentId.Value,
                                Url = item.ImageUrl,
                                Ratio = 1
                            });
                        }

                        await FirebaseHelper.SendPushAsync(item.PushToken, "알림", "프로필 이미지가 승인되었습니다.", "noti:message", null);
                    }
                    break;
                case ConfirmImageTypes.PoomPoomImage:
                    {
                        if (item.ImageId.HasValue)
                        {
                            var imageItem = this.Db.PoomPoom_Images.FirstOrDefault(x => x.Id == item.ImageId.Value);
                            imageItem.Url = item.ImageUrl;
                        }
                        else
                        {
                            this.Db.PoomPoom_Images.Add(new Database.Tables.PoomPoom_Image
                            {
                                CreateTime = DateTime.Now,
                                PoomPoomId = item.ContentId.Value,
                                Url = item.ImageUrl,
                            });
                        }

                        await FirebaseHelper.SendPushAsync(item.PushToken, "알림", "뿜뿜 컨텐츠 이미지가 승인되었습니다.", "noti:message", null);
                    }
                    break;
            }

            this.Db.Member_ConfirmImages.Remove(new Database.Tables.Member_ConfirmImage
            {
                Id = item.ItemId
            });
            await this.Db.SaveChangesAsync();
            await this.Js.InvokeVoidAsync("pageReload");
        }

        private async Task DenyItemAsync(ImageConfirmItem item)
        {
            if (!await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 거부하시겠습니까?"))
                return;

            this.Db.Member_ConfirmImages.Remove(new Database.Tables.Member_ConfirmImage
            {
                Id = item.ItemId
            });

            switch (item.ImageType)
            {
                default:
                case ConfirmImageTypes.ProfileImage:
                    await FirebaseHelper.SendPushAsync(item.PushToken, "알림", "프로필 이미지가 거부되었습니다.", "noti:message", null);
                    break;
                case ConfirmImageTypes.PoomPoomImage:
                    this.Db.PoomPooms.Remove(new Database.Tables.PoomPoom
                    {
                        Id = item.ContentId.Value
                    });
                    await FirebaseHelper.SendPushAsync(item.PushToken, "알림", "뿜뿜 컨텐츠 이미지가 거부되었습니다.", "noti:message", null);
                    break;
            }

            await this.Db.SaveChangesAsync();
            await this.Js.InvokeVoidAsync("pageReload");
        }

        public void Dispose()
        {
            this.Navigation.LocationChanged -= this.Navigation_LocationChanged;
            this.IsDispose = true;
        }
    }

    public class ImageConfirmItem
    {
        public int ItemId { get; internal set; }
        public int? ImageId { get; internal set; }
        public ConfirmImageTypes ImageType { get; internal set; }
        public string ImageUrl { get; internal set; }
        public int MemberId { get; internal set; }
        public int? ContentId { get; set; }
        public string Nickname { get; internal set; }
        public string PushToken { get; internal set; }
        public DateTime CreateTime { get; internal set; }
    }
}
