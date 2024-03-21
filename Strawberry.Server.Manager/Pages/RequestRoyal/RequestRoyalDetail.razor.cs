using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Strawberry.Server.Manager.Pages.RequestRoyal
{
    [Authorize(Roles = "Administrator")]
    public partial class RequestRoyalDetail
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Parameter] public int Id { get; set; }


        private bool IsInit { get; set; }


        private Database.Tables.Member_RequestRoyal Item { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.GetPageData();
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void GetPageData()
        {
            this.Item = this.Db.Member_RequestRoyals
                .Include(x => x.Member)
                .Where(x => x.Id == this.Id)
                .FirstOrDefault();
        }

        private async Task AcceptItemAsync()
        {
            try
            {
                this.Db.SaveChanges();

                await this.Js.InvokeVoidAsync("alert", "적용되었습니다.");
                await this.Js.InvokeVoidAsync("openerReload");
                await this.Js.InvokeVoidAsync("close");
            }
            catch (Exception ex)
            {
                await this.Js.InvokeVoidAsync("alert", ex.Message);
            }
        }
    }
}
