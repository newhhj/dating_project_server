using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Strawberry.Server.Manager.Helpers;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Newtonsoft.Json;

namespace Strawberry.Server.Manager.Pages.AD
{
    [Authorize(Roles = "Administrator")]
    public partial class ADDetail
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] ImageHelper ImageHelper { get; set; }
        [Inject] IHttpContextAccessor HttpContextAccessor { get; set; }
        [Parameter] public int Id { get; set; }

        private bool IsInit { get; set; }

        private Database.Tables.Member[] MemberList { get; set; }
        private ADData PageItem { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.GetPageItemData();
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void GetPageItemData()
        {
            this.MemberList = this.Db.Members
                .Select(x => new Database.Tables.Member
                {
                    Id = x.Id,
                    Nickname = x.Nickname
                })
                .ToArray();

            if (this.Id != 0)
            {
                this.PageItem = this.Db.ADDatas
                    .Where(x => x.Id == this.Id)
                    .FirstOrDefault();
            }
            else
            {
                this.PageItem = new ADData();
            }
        }

        private void OpenWindowDetailImage(PoomPoom_Image item)
        {
            try
            {
                this.Js.InvokeVoidAsync("open", item.Url, "detailimage", "width=450,height=450");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void ImageUpload(InputFileChangeEventArgs e)
        {
            try
            {
                if (e.File.Size > 16777216)
                    throw new Exception("이미지 용량이 너무 큽니다.\n10mb 이하의 이미지를 선택하세요.");

                using var stream = e.File.OpenReadStream(maxAllowedSize: 16777216);
                var result = await this.ImageHelper.SaveImageFromStreamAsync(stream, "images/ad", e.File.Name);

                var scheme = HttpContextAccessor.HttpContext.Request.Scheme;
                var host = HttpContextAccessor.HttpContext.Request.Host;


                switch (this.PageItem.ADType)
                {
                    case ADTypes.MainPopup:
                    case ADTypes.SettingBanner:
                        this.PageItem.JsonData = JsonConvert.SerializeObject(new
                        {
                            ImageUrl = $"{scheme}://{host}{result.url}"
                        });
                        break;
                    default:
                        break;
                }

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void ImageRemove()
        {
            try
            {
                var scheme = HttpContextAccessor.HttpContext.Request.Scheme;
                var host = HttpContextAccessor.HttpContext.Request.Host;

                switch (this.PageItem.ADType)
                {
                    case ADTypes.MainPopup:
                    case ADTypes.SettingBanner:
                        this.PageItem.JsonData = null;
                        break;
                    default:
                        break;
                }

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void AcceptMemberAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.PageItem.ADName))
                    throw new Exception("광고명을 입력하세요");

                if (this.PageItem.ADType == ADTypes.MainPopup ||
                    this.PageItem.ADType == ADTypes.SettingBanner)
                {
                    if (string.IsNullOrWhiteSpace(this.PageItem.JsonData))
                        throw new Exception("이미지를 선택하세요");

                    var data = JsonConvert.DeserializeAnonymousType(this.PageItem.JsonData, new
                    {
                        ImageUrl = default(string)
                    });

                    if (string.IsNullOrWhiteSpace(data.ImageUrl))
                        throw new Exception("이미지를 선택하세요");
                }

                if (this.PageItem.Id == 0)
                {
                    await this.Db.ADDatas.AddAsync(this.PageItem);
                    await this.Db.SaveChangesAsync();
                    await this.Js.InvokeVoidAsync("alert", "등록되었습니다.");
                    await this.Js.InvokeVoidAsync("openerReload");
                    await this.Js.InvokeVoidAsync("close");
                }
                else
                {
                    await this.Db.SaveChangesAsync();
                    await this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
                    await this.Js.InvokeVoidAsync("openerReload");
                    await this.Js.InvokeVoidAsync("close");
                }
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }
}
