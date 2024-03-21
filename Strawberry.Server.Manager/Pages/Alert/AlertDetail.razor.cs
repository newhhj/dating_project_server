using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Strawberry.Server.Database.Tables;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Strawberry.Server.Manager.Pages.Alert
{
    [Authorize(Roles = "Administrator")]
    public partial class AlertDetail
    {
        [Inject] DatabaseContext Db { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Parameter] public int ContentType { get; set; }
        [Parameter] public int Id { get; set; }


        private bool IsInit { get; set; }
        private object Item { get; set; }

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
            this.Item = default(dynamic);

            switch (this.ContentType)
            {
                case 0:
                {
                    this.Item = this.Db.Member_AlertProfiles
                        .Where(x => x.Id == this.Id)
                        .Include(x => x.Member)
                        .Include(x => x.Partner)
                        .FirstOrDefault();
                    break;
                }
                case 1:
                {
                    this.Item = this.Db.Member_AlertPoomPooms
                        .Where(x => x.Id == this.Id)
                        .Include(x => x.Member)
                        .Include(x => x.PoomPoom)
                        .ThenInclude(x => x.Member)
                        .FirstOrDefault();
                    break;
                }
                case 2:
                {
                    this.Item = this.Db.Member_AlertComments
                        .Where(x => x.Id == this.Id)
                        .Include(x => x.Member)
                        .Include(x => x.AlertComment)
                        .ThenInclude(x => x.Member)
                        .FirstOrDefault();
                    break;
                }
                default:
                    break;
            }
        }
        private void OpenDetailWindowOwner()
        {
            switch (this.ContentType)
            {
                case 0:
                    {
                        var item = (Member_AlertProfile)this.Item;
                        this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.MemberId}", "detailmember", "left=0,top=0,width=500,height=720");
                        break;
                    }
                case 1:
                    {
                        var item = (Member_AlertPoomPoom)this.Item;
                        this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.MemberId}", "detailmember", "left=0,top=0,width=500,height=720");
                        break;
                    }
                case 2:
                    {
                        var item = (Member_AlertComment)this.Item;
                        this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.MemberId}", "detailmember", "left=0,top=0,width=500,height=720");
                        break;
                    }
                default:
                    break;
            }
        }

        private void OpenDetailWindowTarget()
        {
            switch (this.ContentType)
            {
                case 0:
                {
                    var item = (Member_AlertProfile)this.Item;
                    this.Js.InvokeVoidAsync("open", $"/memberdetail/{item.PartnerId}", "detailmember", "left=0,top=0,width=500,height=720");
                    break;
                }
                case 1:
                {
                    var item = (Member_AlertPoomPoom)this.Item;
                    this.Js.InvokeVoidAsync("open", $"/poompoomdetail/{item.PoomPoomId}", "poompoomdetail", "left=0,top=0,width=500,height=720");
                    break;
                }
                case 2:
                {
                    var item = (Member_AlertComment)this.Item;
                    this.Js.InvokeVoidAsync("open", $"/poompoomdetail/{item.AlertComment.PoomPoomId}", "poompoomdetail", "left=0,top=0,width=500,height=720");
                    break;
                }
                default:
                    break;
            }
        }
    }
}
