using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Strawberry.Server.Database;
using Strawberry.Server.Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Pages.PushMessage
{
    [Authorize(Roles = "Administrator")]
    public partial class PushMessage
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] FirebaseHelper FirebaseHelper { get; set; }


        public bool IsInit { get; set; } = false;


        public Database.GenderTypes? SelectGenderType { get; set; }
        public string Message { get; set; }
        public string ButtonText { get; set; } = "발송";


        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                this.IsInit = true;
                this.StateHasChanged();
            }

            base.OnAfterRender(firstRender);
        }

        private async Task SendMessageAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.Message))
                    throw new Exception("메세지 내용을 입력하세요");

                var query = this.Db.Members
                    .Where(x => !string.IsNullOrWhiteSpace(x.PushToken));

                if (this.SelectGenderType.HasValue)
                    query = query
                        .Where(x => x.Gender == this.SelectGenderType.Value);

                var members = await query
                    .Select(x => new
                    {
                        x.Id,
                        x.PushToken
                    })
                    .ToArrayAsync();

                var list = new List<string>();

                for (int i = 0; i < members.Length; i++)
                {
                    var member = members[i];
                    this.Db.Member_Notifications.Add(new Database.Tables.Member_Notification
                    {
                        IsShow = false,
                        MemberId = members[i].Id,
                        Message = this.Message
                    });
                    await this.Db.SaveChangesAsync();
                    list.Add(members[i].PushToken);

                    if (list.Count == 100 || i + 1 == members.Length)
                    {
                        var tokenList = list.ToArray();
                        await FirebaseHelper.SendPushAsync(tokenList, "알림", this.Message, "noti:message", null);
                    }

                    this.ButtonText = $"{i + 1:#,##0} / {members.Length:#,##0}";
                    this.StateHasChanged();
                }

                await this.JS.InvokeVoidAsync("alert", "메세지를 발송했습니다.");
            }
            catch (Exception ex)
            {
                await this.JS.InvokeVoidAsync("alert", ex.Message);
            }

            this.ButtonText = "발송";
        }
    }
}
