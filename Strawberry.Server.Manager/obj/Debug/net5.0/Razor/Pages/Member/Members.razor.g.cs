#pragma checksum "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48c22601b0494391067a3cf5e694eba12e171097"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.Member
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Strawberry.Server.Manager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\_Imports.razor"
using Strawberry.Server.Manager.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
           [Authorize(Roles = "Administrator")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/members")]
    public partial class Members : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 4 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\">\r\n        데이터를 불러오는 중입니다.<br>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 10 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<span class=\"label-title\">회원 목록</span>\r\n    ");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "context-box");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "filter-box");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "filter-item");
            __builder.AddMarkupContent(8, "<span class=\"label-name\" style=\"margin-left:10px;\">성별</span>\r\n                ");
            __builder.OpenElement(9, "select");
            __builder.AddAttribute(10, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 18 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                        () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(11, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 18 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                this.QueryItem.Gender

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.Gender = __value, this.QueryItem.Gender));
            __builder.SetUpdatesAttributeName("value");
            __builder.OpenElement(13, "option");
            __builder.AddAttribute(14, "value");
            __builder.AddMarkupContent(15, "전체");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n                    ");
            __builder.OpenElement(17, "option");
            __builder.AddAttribute(18, "value", 
#nullable restore
#line 20 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.GenderTypes.Male

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(19, "남성");
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n                    ");
            __builder.OpenElement(21, "option");
            __builder.AddAttribute(22, "value", 
#nullable restore
#line 21 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.GenderTypes.Female

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(23, "여성");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n            ");
            __builder.OpenElement(25, "div");
            __builder.AddAttribute(26, "class", "filter-item");
            __builder.AddMarkupContent(27, "<span class=\"label-name\" style=\"margin-left:10px;\">등급</span>\r\n                ");
            __builder.OpenElement(28, "select");
            __builder.AddAttribute(29, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 26 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                           () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(30, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 26 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                this.QueryItem.LevelType

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(31, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.LevelType = __value, this.QueryItem.LevelType));
            __builder.SetUpdatesAttributeName("value");
            __builder.OpenElement(32, "option");
            __builder.AddAttribute(33, "value");
            __builder.AddMarkupContent(34, "전체");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n                    ");
            __builder.OpenElement(36, "option");
            __builder.AddAttribute(37, "value", 
#nullable restore
#line 28 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.LevelTypes.Silver

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(38, "실버");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n                    ");
            __builder.OpenElement(40, "option");
            __builder.AddAttribute(41, "value", 
#nullable restore
#line 29 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.LevelTypes.Gold

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(42, "골드");
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n                    ");
            __builder.OpenElement(44, "option");
            __builder.AddAttribute(45, "value", 
#nullable restore
#line 30 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.LevelTypes.Platinum

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(46, "플래티넘");
            __builder.CloseElement();
            __builder.AddMarkupContent(47, "\r\n                    ");
            __builder.OpenElement(48, "option");
            __builder.AddAttribute(49, "value", 
#nullable restore
#line 31 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.LevelTypes.Diamond

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(50, "다이아몬드");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(51, "\r\n            ");
            __builder.OpenElement(52, "div");
            __builder.AddAttribute(53, "class", "filter-item");
            __builder.AddMarkupContent(54, "<span class=\"label-name\" style=\"margin-left:10px;\">상태</span>\r\n                ");
            __builder.OpenElement(55, "select");
            __builder.AddAttribute(56, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 36 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                             () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(57, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 36 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                this.QueryItem.MemberState

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(58, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.MemberState = __value, this.QueryItem.MemberState));
            __builder.SetUpdatesAttributeName("value");
            __builder.OpenElement(59, "option");
            __builder.AddAttribute(60, "value");
            __builder.AddMarkupContent(61, "전체");
            __builder.CloseElement();
            __builder.AddMarkupContent(62, "\r\n                    ");
            __builder.OpenElement(63, "option");
            __builder.AddAttribute(64, "value", 
#nullable restore
#line 38 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.MemberStateTypes.Normal

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(65, "일반");
            __builder.CloseElement();
            __builder.AddMarkupContent(66, "\r\n                    ");
            __builder.OpenElement(67, "option");
            __builder.AddAttribute(68, "value", 
#nullable restore
#line 39 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.MemberStateTypes.JoinWait

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(69, "가입대기");
            __builder.CloseElement();
            __builder.AddMarkupContent(70, "\r\n                    ");
            __builder.OpenElement(71, "option");
            __builder.AddAttribute(72, "value", 
#nullable restore
#line 40 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.MemberStateTypes.JoinConfirm

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(73, "가입승인");
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n                    ");
            __builder.OpenElement(75, "option");
            __builder.AddAttribute(76, "value", 
#nullable restore
#line 41 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.MemberStateTypes.JoinDeny

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(77, "가입반려");
            __builder.CloseElement();
            __builder.AddMarkupContent(78, "\r\n                    ");
            __builder.OpenElement(79, "option");
            __builder.AddAttribute(80, "value", 
#nullable restore
#line 42 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     (int)Database.MemberStateTypes.Block

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(81, "차단");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(82, "\r\n            ");
            __builder.OpenElement(83, "div");
            __builder.AddAttribute(84, "class", "filter-item");
            __builder.AddMarkupContent(85, "<span class=\"label-name\" style=\"margin-left:10px;\">특수</span>\r\n                ");
            __builder.OpenElement(86, "select");
            __builder.AddAttribute(87, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.ChangeEventArgs>(this, 
#nullable restore
#line 47 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                         () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(88, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 47 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                this.QueryItem.EtcType

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(89, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.EtcType = __value, this.QueryItem.EtcType));
            __builder.SetUpdatesAttributeName("value");
            __builder.OpenElement(90, "option");
            __builder.AddAttribute(91, "value");
            __builder.AddMarkupContent(92, "전체");
            __builder.CloseElement();
            __builder.AddMarkupContent(93, "\r\n                    ");
            __builder.OpenElement(94, "option");
            __builder.AddAttribute(95, "value", "10");
            __builder.AddContent(96, "10%");
            __builder.CloseElement();
            __builder.AddMarkupContent(97, "\r\n                    ");
            __builder.OpenElement(98, "option");
            __builder.AddAttribute(99, "value", "VIP");
            __builder.AddContent(100, "VIP");
            __builder.CloseElement();
            __builder.AddMarkupContent(101, "\r\n                    ");
            __builder.OpenElement(102, "option");
            __builder.AddAttribute(103, "value", "Royal");
            __builder.AddMarkupContent(104, "로얄");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(105, "\r\n            ");
            __builder.OpenElement(106, "div");
            __builder.AddAttribute(107, "class", "filter-item");
            __builder.AddMarkupContent(108, "<span class=\"label-name\" style=\"margin-left:10px;\">검색</span>\r\n                ");
            __builder.OpenElement(109, "div");
            __builder.AddAttribute(110, "class", "search");
            __builder.OpenElement(111, "input");
            __builder.AddAttribute(112, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 57 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                            (e) => { if (e.Key == "Enter") { this.QueryItem.Page = 1; this.MovePage(); } }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(113, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 57 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                   this.QueryItem.Search

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(114, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.Search = __value, this.QueryItem.Search));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(115, "\r\n                    ");
            __builder.OpenElement(116, "button");
            __builder.AddAttribute(117, "class", "accept");
            __builder.AddAttribute(118, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 58 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                     () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(119, "<i class=\"fas fa-search\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(120, "\r\n        ");
            __builder.OpenElement(121, "table");
            __builder.AddAttribute(122, "cellpadding", "0");
            __builder.AddAttribute(123, "cellspacing", "0");
            __builder.AddMarkupContent(124, @"<thead><tr><th>
                        닉네임
                    </th>
                    <th>
                        성별
                    </th>
                    <th>
                        등급
                    </th>
                    <th>
                        VIP
                    </th>
                    <th>
                        로얄
                    </th>
                    <th>
                        상태
                    </th>
                    <th>
                        가입일
                    </th></tr></thead>
            ");
            __builder.OpenElement(125, "tbody");
#nullable restore
#line 91 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                 if (this.Items == null || this.Items.Length == 0)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(126, "<tr><td colspan=\"7\"><div class=\"no-list\">\r\n                                표시할 목록이 없습니다.\r\n                            </div></td></tr>");
#nullable restore
#line 100 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                }
                else
                {
                    foreach (var item in this.Items)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(127, "tr");
            __builder.OpenElement(128, "td");
            __builder.OpenElement(129, "span");
            __builder.AddAttribute(130, "style", "font-weight: bold; cursor: pointer;");
            __builder.AddAttribute(131, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 107 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                            () => { this.OpenDetail(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(132, 
#nullable restore
#line 108 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     item.Nickname

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(133, "\r\n                            ");
            __builder.OpenElement(134, "td");
            __builder.AddContent(135, 
#nullable restore
#line 112 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                  item.Gender == Database.GenderTypes.Male ? "남성" : "여성"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(136, "\r\n                            ");
            __builder.OpenElement(137, "td");
#nullable restore
#line 115 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                 switch (item.LevelType)
                                {
                                    case Database.LevelTypes.Platinum:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(138, "<span>플래티넘</span>");
#nullable restore
#line 119 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.LevelTypes.Diamond:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(139, "<span>다이아몬드</span>");
#nullable restore
#line 122 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.LevelTypes.Gold:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(140, "<span>골드</span>");
#nullable restore
#line 125 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.LevelTypes.Silver:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(141, "<span>실버</span>");
#nullable restore
#line 128 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    default:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(142, "<span>-</span>");
#nullable restore
#line 131 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(143, "\r\n                            ");
            __builder.OpenElement(144, "td");
            __builder.AddContent(145, 
#nullable restore
#line 135 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                  item.IsVIP ? "VIP" : "-"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(146, "\r\n                            ");
            __builder.OpenElement(147, "td");
            __builder.AddContent(148, 
#nullable restore
#line 138 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                  item.IsRoyal ? "Royal" : "-"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(149, "\r\n                            ");
            __builder.OpenElement(150, "td");
#nullable restore
#line 141 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                 switch (item.MemberState)
                                {
                                    case Database.MemberStateTypes.Normal:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(151, "<span>일반</span>");
#nullable restore
#line 145 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.MemberStateTypes.Block:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(152, "<span>차단</span>");
#nullable restore
#line 148 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.MemberStateTypes.JoinConfirm:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(153, "<span>가입승인</span>");
#nullable restore
#line 151 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.MemberStateTypes.JoinWait:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(154, "<span>가입대기</span>");
#nullable restore
#line 154 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    case Database.MemberStateTypes.JoinDeny:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(155, "<span>가입반려</span>");
#nullable restore
#line 157 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                    default:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(156, "<span>-</span>");
#nullable restore
#line 160 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                        break;
                                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(157, "\r\n                            ");
            __builder.OpenElement(158, "td");
            __builder.AddContent(159, 
#nullable restore
#line 164 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                 item.CreateTime.ToString("yyyy.MM.dd")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 167 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(160, "\r\n            ");
            __builder.OpenElement(161, "tfoot");
            __builder.OpenElement(162, "tr");
            __builder.OpenElement(163, "td");
            __builder.AddAttribute(164, "colspan", "7");
            __builder.OpenElement(165, "div");
            __builder.AddAttribute(166, "class", "pager");
            __builder.OpenElement(167, "btn");
            __builder.AddAttribute(168, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 174 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                           () => { this.QueryItem.Page--; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(169, "<i class=\"fas fa-chevron-left\"></i>");
            __builder.CloseElement();
            __builder.AddMarkupContent(170, "\r\n                            ");
            __builder.OpenElement(171, "input");
            __builder.AddAttribute(172, "type", "number");
            __builder.AddAttribute(173, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 177 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                                                                               (e) => { if (e.Key == "Enter") this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(174, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 177 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                                        this.QueryItem.Page

#line default
#line hidden
#nullable disable
            , culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(175, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.Page = __value, this.QueryItem.Page, culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(176, "\r\n                            ");
            __builder.OpenElement(177, "span");
            __builder.AddContent(178, "/ ");
            __builder.AddContent(179, 
#nullable restore
#line 178 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                     this.QueryItem.TotalPageCount.ToString("#,##0")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(180, "\r\n                            ");
            __builder.OpenElement(181, "btn");
            __builder.AddAttribute(182, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 179 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
                                           () => { this.QueryItem.Page++; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(183, "<i class=\"fas fa-chevron-right\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 188 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Member\Members.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591