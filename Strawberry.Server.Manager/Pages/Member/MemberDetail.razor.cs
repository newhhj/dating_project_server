using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Member
{
    public partial class MemberDetail : IDisposable
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] FirebaseHelper FirebaseHelper { get; set; }

        [Parameter] public int Id { get; set; }

        private bool IsInit { get; set; }
        private bool IsDispose { get; set; }
        private Database.Tables.Member MemberItem { get; set; }
        private MemberStateTypes MemberState { get; set; }
        private LevelTypes LevelType { get; set; }
        private double LevelPoint { get; set; }
        private bool IsRoyal { get; set; }
        private bool IsVIP { get; set; }
        private bool HasStarBadge { get; set; }
        private int Point { get; set; }
        private int RateCharming { get; set; }
        private int RateResponse { get; set; }
        private double StarPoint { get; set; }

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
            this.MemberItem = this.Db.Members
                .Where(x => x.Id == this.Id)
                .Include(x => x.Member_Account)
                .Include(x => x.Member_ProfileImages)
                .Include(x => x.Member_CharmingPoints)
                .Include(x => x.Member_Interests)
                .FirstOrDefault();

            this.MemberState = this.MemberItem.MemberState;
            this.LevelType = this.MemberItem.LevelType;
            this.LevelPoint = this.MemberItem.LevelPoint;
            this.IsRoyal = this.MemberItem.IsRoyal;
            this.IsVIP = this.MemberItem.IsVIP;
            this.HasStarBadge = this.MemberItem.HasStarBadge;
            this.Point = this.MemberItem.Point;
            this.RateCharming = this.MemberItem.RateCharming;
            this.RateResponse = this.MemberItem.RateResponse;

            var starPointQuery = this.Db.Member_StarPoints
                .Where(x => x.PartnerId == this.MemberItem.Id);
            if (starPointQuery.Count() > 0)
            {
                this.StarPoint = starPointQuery.Average(x => x.StarPoint);
            }
        }

        private async void UpdateMemberState()
        {
            try
            {
                this.MemberItem.MemberState = this.MemberState;
                this.Db.SaveChanges();

                try
                {
                    if (this.MemberState == MemberStateTypes.JoinConfirm)
                    {
                        await FirebaseHelper.SendPushAsync(this.MemberItem.PushToken, "알림", "회원 가입이 승인되었습니다.", "noti:message", null);
                    }
                    else if (this.MemberState == MemberStateTypes.JoinDeny)
                    {
                        await FirebaseHelper.SendPushAsync(this.MemberItem.PushToken, "알림", "회원 가입이 거부되었습니다.", "noti:message", null);
                    }
                }
                catch { }

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void RemoveProfileImage(Database.Tables.Member_ProfileImage item)
        {
            try
            {
                this.MemberItem.Member_ProfileImages.Remove(item);
                this.Db.SaveChanges();

                throw new Exception("삭제되었습니다.");
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

        private void UpdateLevelType()
        {
            try
            {
                this.MemberItem.LevelType = this.LevelType;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateLevelPoint()
        {
            try
            {
                this.MemberItem.LevelPoint = this.LevelPoint;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateIsRoyal()
        {
            try
            {
                this.MemberItem.IsRoyal = this.IsRoyal;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateIsVIP()
        {
            try
            {
                this.MemberItem.IsVIP = this.IsVIP;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateHasStarBadge()
        {
            try
            {
                this.MemberItem.HasStarBadge = this.HasStarBadge;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdatePoint()
        {
            try
            {
                using (var tran = this.Db.Database.BeginTransaction())
                {
                    var acceptPoint = this.Point - this.MemberItem.Point;
                    this.MemberItem.Point = this.Point;
                    this.Db.SaveChanges();

                    this.Db.Add(new Member_PointLog
                    {
                        MemberId = this.MemberItem.Id,
                        AcceptPoint = acceptPoint,
                        CurrentPoint = this.MemberItem.Point,
                        Comment = "관리자 보너스",
                        CreateTime = DateTime.Now,
                    });
                    this.Db.SaveChanges();

                    tran.Commit();
                    throw new Exception("적용되었습니다.");
                }
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateRateCharming()
        {
            try
            {
                this.MemberItem.RateCharming = this.RateCharming;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void UpdateRateResponse()
        {
            try
            {
                this.MemberItem.RateResponse = this.RateResponse;
                this.Db.SaveChanges();

                throw new Exception("적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        public void Dispose()
        {
            this.IsDispose = false;
        }
    }
}
