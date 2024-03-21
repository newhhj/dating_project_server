using Microsoft.AspNetCore.Authorization;
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

namespace Strawberry.Server.Manager.Pages.Manager
{
    [Authorize(Roles = "Administrator")]
    public partial class ManagerListItem
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Parameter] public int? Id { get; set; }

        private bool IsInit { get; set; }

        private Database.Tables.Manager Item { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.GetItem();
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void GetItem()
        {
            if (this.Id.HasValue)
            {
                this.Item = this.Db.Managers
                    .Where(x => x.Id == this.Id)
                    .FirstOrDefault();
            }
            else
            {
                this.Item = new Database.Tables.Manager();
            }
        }

        private async void AcceptMemberAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.Item.Nickname))
                    throw new Exception("닉네임을 입력하세요");
                if (string.IsNullOrWhiteSpace(this.Item.UserId))
                    throw new Exception("아이디를 입력하세요");
                if (string.IsNullOrWhiteSpace(this.Item.Passwd))
                    throw new Exception("비밀번호를 입력하세요");
                if (await this.Db.Managers.AnyAsync(x => x.Id != this.Item.Id && x.Nickname == this.Item.Nickname))
                    throw new Exception("사용할 수 없는 닉네임 입니다.");
                if (await this.Db.Managers.AnyAsync(x => x.Id != this.Item.Id && x.UserId == this.Item.UserId))
                    throw new Exception("사용할 수 없는 아이디 입니다.");

                if (this.Item.Id == 0)
                {
                    await this.Db.Managers.AddAsync(this.Item);
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
