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

namespace Strawberry.Server.Manager.Pages.PoomPoom
{
    [Authorize(Roles = "Administrator")]
    public partial class PoomPoomDetail
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] ImageHelper ImageHelper { get; set; }
        [Inject] IHttpContextAccessor HttpContextAccessor { get; set; }
        [Parameter] public int Id { get; set; }

        private bool IsInit { get; set; }

        private Database.Tables.Member[] MemberList { get; set; }
        private PoomPoomDetailItem PageItem { get; set; }

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
                this.PageItem = this.Db.PoomPooms
                    .Where(x => x.Id == this.Id)
                    .Include(x => x.PoomPoom_Images)
                    .Include(x => x.PoomPoom_Comments)
                    .ThenInclude(x => x.Member)
                    .Select(x => new PoomPoomDetailItem
                    {
                        Item = x,
                        Nickname = x.Member.Nickname
                    })
                    .FirstOrDefault();
            }
            else
            {
                this.PageItem = new PoomPoomDetailItem
                {
                    Item = new Database.Tables.PoomPoom
                    {
                        PoomPoom_Images = new List<PoomPoom_Image>()
                    }
                };
            }
        }

        private void RemoveProfileImage(PoomPoom_Image item)
        {
            try
            {
                this.PageItem.Item.PoomPoom_Images?.Remove(item);
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
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
                var result = await this.ImageHelper.SaveImageFromStreamAsync(stream, "images/poompoom", e.File.Name);

                var scheme = HttpContextAccessor.HttpContext.Request.Scheme;
                var host = HttpContextAccessor.HttpContext.Request.Host;

                this.PageItem.Item.PoomPoom_Images.Add(new PoomPoom_Image
                {
                    CreateTime = DateTime.Now,
                    Url = $"{scheme}://{host}{result.url}",
                });

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void RemoveComment(Database.Tables.PoomPoom_Comment item)
        {
            if (!await this.Js.InvokeAsync<bool>("confirm", "해당 항목을 삭제하시겠습니까?"))
                return;

            this.Db.PoomPoom_Comments.Remove(item);
            await this.Db.SaveChangesAsync();

            this.PageItem.Item.PoomPoom_Comments.Remove(item);
            this.StateHasChanged();
        }

        private void OpenCommentMember(int? id)
        {
            if (id.HasValue)
                this.Js.InvokeVoidAsync("open", $"/memberdetail/{id}", "detailmember", "left=0,top=0,width=500,height=720");
        }

        private async void AcceptMemberAsync()
        {
            try
            {
                if (this.PageItem.Item.MemberId == 0)
                    throw new Exception("닉네임을 선택하세요");
                if (this.PageItem.Item.PoomPoom_Images.Count == 0)
                    throw new Exception("이미지를 선택하세요");
                if (this.PageItem.Item.ContentType == ContentTypes.Metting)
                {
                    if (string.IsNullOrWhiteSpace(this.PageItem.Item.Area))
                        throw new Exception("장소를 입력하세요");
                    if (string.IsNullOrWhiteSpace(this.PageItem.Item.Time))
                        throw new Exception("시간을 입력하세요");
                }
                if (this.PageItem.Item.ContentType == ContentTypes.Sell)
                {
                    if (string.IsNullOrWhiteSpace(this.PageItem.Item.Area))
                        throw new Exception("장소를 입력하세요");
                }

                if (this.PageItem.Item.Id == 0)
                {
                    await this.Db.PoomPooms.AddAsync(this.PageItem.Item);
                    await this.Db.SaveChangesAsync();
                    await this.Js.InvokeVoidAsync("alert", "등록되었습니다.");
                    await this.Js.InvokeVoidAsync("openerReload");
                    await this.Js.InvokeVoidAsync("close");
                }
                else
                {
                    await this.Db.SaveChangesAsync();
                    await this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
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
