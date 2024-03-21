using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Caller
{
    public partial class CallersDetail
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] ImageHelper ImageHelper { get; set; }
        [Inject] IHttpContextAccessor HttpContextAccessor { get; set; }
        [Parameter] public int? Id { get; set; }

        private bool IsInit { get; set; }

        private Database.Tables.Member MemberItem { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.GetMemberData();
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void GetMemberData()
        {
            if (this.Id.HasValue)
            {
                this.MemberItem = this.Db.Members
                    .Where(x => x.Id == this.Id)
                    .Include(x => x.Member_ProfileImages)
                    .Include(x => x.Member_CharmingPoints)
                    .Include(x => x.Member_Interests)
                    .FirstOrDefault();
            }
            else
            {
                this.MemberItem = new Database.Tables.Member
                {
                    Member_ProfileImages = new List<Member_ProfileImage>(),
                    Member_CharmingPoints = new List<Member_CharmingPoint>(),
                    Member_Interests = new List<Member_Interest>()
                };
            }
        }

        private void RemoveProfileImage(Database.Tables.Member_ProfileImage item)
        {
            try
            {
                this.MemberItem.Member_ProfileImages.Remove(item);
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void OpenWindowDetailImage(Database.Tables.Member_ProfileImage item)
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
                var result = await this.ImageHelper.SaveImageFromStreamAsync(stream, "images/profile", e.File.Name);

                var scheme = HttpContextAccessor.HttpContext.Request.Scheme;
                var host = HttpContextAccessor.HttpContext.Request.Host;

                this.MemberItem.Member_ProfileImages.Add(new Member_ProfileImage
                {
                    CreateTime = DateTime.Now,
                    Path = result.local,
                    Url = $"{scheme}://{host}{result.url}"
                });

                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void ToggleCharmingPoint(string name)
        {
            try
            {
                var item = this.MemberItem.Member_CharmingPoints.FirstOrDefault(x => x.Name == name);
                if (item == null)
                {
                    if (this.MemberItem.Member_CharmingPoints.Count >= 3)
                        throw new Exception("최대 3개까지만 선택할 수 있습니다.");

                    this.MemberItem.Member_CharmingPoints.Add(new Member_CharmingPoint
                    {
                        Name = name
                    });
                }
                else
                {
                    this.MemberItem.Member_CharmingPoints.Remove(item);
                }
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private async void ToggleInterest(string name)
        {
            try
            {
                var item = this.MemberItem.Member_Interests.FirstOrDefault(x => x.Name == name);
                if (item == null)
                {
                    if (this.MemberItem.Member_Interests.Count >= 3)
                        throw new Exception("최대 3개까지만 선택할 수 있습니다.");

                    this.MemberItem.Member_Interests.Add(new Member_Interest
                    {
                        Name = name
                    });
                }
                else
                {
                    this.MemberItem.Member_Interests.Remove(item);
                }
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
                if (string.IsNullOrWhiteSpace(this.MemberItem.Nickname))
                    throw new Exception("닉네임을 입력하세요");
                if (this.MemberItem.Member_ProfileImages.Count < 2)
                    throw new Exception("프로필 이미지를 2개 이상 등록하세요");
                if (this.MemberItem.BirthDay.Year < 1900)
                    throw new Exception("생년월일을 선택하세요");
                if (this.MemberItem.Tall < 150 || this.MemberItem.Tall > 191)
                    throw new Exception("키는 150 이상 191 이하로 입력하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.BodyStyle))
                    throw new Exception("체형을 선택하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.School))
                    throw new Exception("학력을 선택하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.Job))
                    throw new Exception("직업을 입력하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.Religion))
                    throw new Exception("종교를 선택하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.Alcohol))
                    throw new Exception("음주를 선택하세요");
                if (string.IsNullOrWhiteSpace(this.MemberItem.Smoking))
                    throw new Exception("흡연을 선택하세요");
                if (this.MemberItem.Lat == 0)
                    throw new Exception("위도를 입력하세요");
                if (this.MemberItem.Lng == 0)
                    throw new Exception("경도를 입력하세요");
                if (this.MemberItem.RateCharming == 0)
                    throw new Exception("매력율을 입력하세요");
                if (this.MemberItem.RateResponse == 0)
                    throw new Exception("응답율을 입력하세요");


                if (this.MemberItem.Id == 0)
                {
                    this.MemberItem.ApiKey = Guid.NewGuid().ToString().ToLower();
                    this.MemberItem.MemberState = MemberStateTypes.Normal;
                    this.MemberItem.Platform = "Android";
                    this.MemberItem.CreateTime = DateTime.Now;
                    this.MemberItem.LocationCheckTime = DateTime.Now;
                    this.MemberItem.PrivacyCheckTime = DateTime.Now;
                    this.MemberItem.SensitiveCheckTime = DateTime.Now;
                    this.MemberItem.TermCheckTime = DateTime.Now;
                    this.MemberItem.LastLoginTime = DateTime.Now;
                    this.Db.Members.Add(this.MemberItem);
                }

                await this.Db.SaveChangesAsync();
                await this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }
}
