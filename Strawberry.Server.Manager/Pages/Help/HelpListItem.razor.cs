using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.Help
{
    [Authorize(Roles = "Administrator")]
    public partial class HelpListItem
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Parameter] public int Id { get; set; }


        private bool IsInit { get; set; }


        private HelpMessage Item { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.GetData();
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private void GetData()
        {
            if (this.Id == 0)
            {
                this.Item = new HelpMessage();
            }
            else
            {
                this.Item = this.Db.HelpMessages
                    .Where(x => x.Id == this.Id)
                    .FirstOrDefault();
            }
        }

        private async Task AcceptItemAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.Item.Title))
                    throw new Exception("제목을 입력하세요");
                if (string.IsNullOrWhiteSpace(this.Item.Content))
                    throw new Exception("내용을 입력하세요");

                Console.WriteLine($"[TEST] :: {this.Item.Id}");

                if (this.Item.Id == 0)
                {
                    this.Db.HelpMessages.Add(this.Item);
                    this.Db.SaveChanges();

                    await this.Js.InvokeVoidAsync("alert", "등록되었습니다.");
                    await this.Js.InvokeVoidAsync("openerReload");
                    await this.Js.InvokeVoidAsync("close");
                }
                else
                {
                    this.Db.SaveChanges();

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
