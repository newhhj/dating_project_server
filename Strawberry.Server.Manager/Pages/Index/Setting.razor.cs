using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Index
{
    public partial class Setting
    {
        
        [Inject] private Database.DatabaseContext Db { get; set; }
        [Inject] private IJSRuntime Js { get; set; }

        private bool IsInit { get; set; }
        private Database.Tables.Setting SettingItem { get; set; }
        private string NewAdminId { get; set; }
        private string CurrentPasswd { get; set; }
        private string NewPasswd { get; set; }
        private string RePasswd { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.InitPage();
                this.StateHasChanged();
            }
        }

        private void InitPage()
        {
            this.IsInit = true;
            this.SettingItem = this.Db.Settings.FirstOrDefault();
        }

        private void AcceptAccount()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.CurrentPasswd))
                    throw new Exception("현재 비밀번호를 입력하세요.");

                if (this.CurrentPasswd != this.SettingItem.AdminPw)
                    throw new Exception("현재 비밀번호가 일치하지 않습니다.");

                if (string.IsNullOrWhiteSpace(this.NewPasswd))
                    throw new Exception("새 비밀번호를 입력하세요.");

                if (string.IsNullOrWhiteSpace(this.RePasswd))
                    throw new Exception("확인용 비밀번호를 입력하세요.");

                if (this.NewPasswd != this.RePasswd)
                    throw new Exception("새 비밀번호와 확인용 비밀번호가 일치하지 않습니다.");

                if (this.CurrentPasswd == this.NewPasswd)
                    throw new Exception("현재 비밀번호와 동일한 비밀번호는 새 비밀번호로 이용할 수 없습니다.");

                if (!this.SettingItem.UseUpdateAdminId)
                {
                    if (string.IsNullOrWhiteSpace(this.NewAdminId))
                        throw new Exception("최조 아이디는 반드시 변경해야 합니다.");

                    if (this.SettingItem.AdminId == this.NewAdminId)
                        throw new Exception("기존 아이디는 이용할 수 없습니다.");

                    this.SettingItem.AdminId = this.NewAdminId;
                    this.SettingItem.UseUpdateAdminId = true;
                }

                this.SettingItem.AdminPw = this.NewPasswd;

                this.Db.SaveChanges();

                this.NewAdminId = null;
                this.CurrentPasswd = null;
                this.NewPasswd = null;
                this.RePasswd = null;

                this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
            }
            catch (Exception ex)
            {
                this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }

        private void AcceptSettingItem()
        {
            this.Db.SaveChanges();
            this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
        }
    }
}
