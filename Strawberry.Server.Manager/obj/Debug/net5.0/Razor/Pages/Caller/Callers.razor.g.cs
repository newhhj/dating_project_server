#pragma checksum "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "39aee0c4c08290fb7cf889de1847ea6578c75f49"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.Caller
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
#line 3 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
           [Authorize(Roles = "Administrator")]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/callers")]
    public partial class Callers : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 5 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\">\r\n        데이터를 불러오는 중입니다.<br>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 11 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
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
            __builder.AddMarkupContent(8, "<span class=\"label-name\" style=\"margin-left:10px;\">검색</span>\r\n                ");
            __builder.OpenElement(9, "div");
            __builder.AddAttribute(10, "class", "search");
            __builder.OpenElement(11, "input");
            __builder.AddAttribute(12, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 20 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                                                            (e) => { if (e.Key == "Enter") { this.QueryItem.Page = 1; this.MovePage(); } }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(13, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 20 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                   this.QueryItem.Search

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.Search = __value, this.QueryItem.Search));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n                    ");
            __builder.OpenElement(16, "button");
            __builder.AddAttribute(17, "class", "accept");
            __builder.AddAttribute(18, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 21 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                     () => { this.QueryItem.Page = 1; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(19, "<i class=\"fas fa-search\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n            ");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "style", "flex-grow: 1; text-align: end; padding-right: 10px;");
            __builder.OpenElement(23, "button");
            __builder.AddAttribute(24, "class", "accept");
            __builder.AddAttribute(25, "style", "display: inline-block;");
            __builder.AddAttribute(26, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 27 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                                                OpenDetail

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(27, "신규등록");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(28, "\r\n        ");
            __builder.OpenElement(29, "table");
            __builder.AddAttribute(30, "cellpadding", "0");
            __builder.AddAttribute(31, "cellspacing", "0");
            __builder.AddMarkupContent(32, @"<thead><tr><th>
                        닉네임
                    </th>
                    <th>
                        성별
                    </th>
                    <th>
                        등급
                    </th>
                    <th>
                        가입일
                    </th></tr></thead>
            ");
            __builder.OpenElement(33, "tbody");
#nullable restore
#line 48 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                 if (this.Items == null || this.Items.Length == 0)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(34, "<tr><td colspan=\"4\"><div class=\"no-list\">\r\n                                표시할 목록이 없습니다.\r\n                            </div></td></tr>");
#nullable restore
#line 57 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                }
                else
                {
                    foreach (var item in this.Items)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(35, "tr");
            __builder.OpenElement(36, "td");
            __builder.OpenElement(37, "span");
            __builder.AddAttribute(38, "style", "font-weight: bold; cursor: pointer;");
            __builder.AddAttribute(39, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 64 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                                                            () => { this.OpenDetail(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddContent(40, 
#nullable restore
#line 65 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                     item.Nickname

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\r\n                            ");
            __builder.OpenElement(42, "td");
            __builder.AddContent(43, 
#nullable restore
#line 69 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                  item.Gender == Database.GenderTypes.Male ? "남성" : "여성"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(44, "\r\n                            ");
            __builder.OpenElement(45, "td");
#nullable restore
#line 72 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                 switch (item.LevelType)
                                {
                                    case Database.LevelTypes.Platinum:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(46, "<span>플래티넘</span>");
#nullable restore
#line 76 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                        break;
                                    case Database.LevelTypes.Diamond:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(47, "<span>다이아몬드</span>");
#nullable restore
#line 79 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                        break;
                                    case Database.LevelTypes.Gold:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(48, "<span>골드</span>");
#nullable restore
#line 82 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                        break;
                                    case Database.LevelTypes.Silver:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(49, "<span>실버</span>");
#nullable restore
#line 85 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                        break;
                                    default:

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(50, "<span>-</span>");
#nullable restore
#line 88 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                        break;
                                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(51, "\r\n                            ");
            __builder.OpenElement(52, "td");
            __builder.AddContent(53, 
#nullable restore
#line 92 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                 item.CreateTime.ToString("yyyy.MM.dd")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 95 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\r\n            ");
            __builder.OpenElement(55, "tfoot");
            __builder.OpenElement(56, "tr");
            __builder.OpenElement(57, "td");
            __builder.AddAttribute(58, "colspan", "4");
            __builder.OpenElement(59, "div");
            __builder.AddAttribute(60, "class", "pager");
            __builder.OpenElement(61, "btn");
            __builder.AddAttribute(62, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 102 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                           () => { this.QueryItem.Page--; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(63, "<i class=\"fas fa-chevron-left\"></i>");
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n                            ");
            __builder.OpenElement(65, "input");
            __builder.AddAttribute(66, "type", "number");
            __builder.AddAttribute(67, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 105 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                                                                               (e) => { if (e.Key == "Enter") this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(68, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 105 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                                        this.QueryItem.Page

#line default
#line hidden
#nullable disable
            , culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(69, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.QueryItem.Page = __value, this.QueryItem.Page, culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(70, "\r\n                            ");
            __builder.OpenElement(71, "span");
            __builder.AddContent(72, "/ ");
            __builder.AddContent(73, 
#nullable restore
#line 106 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                     this.QueryItem.TotalPageCount.ToString("#,##0")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n                            ");
            __builder.OpenElement(75, "btn");
            __builder.AddAttribute(76, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 107 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
                                           () => { this.QueryItem.Page++; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(77, "<i class=\"fas fa-chevron-right\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 116 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Caller\Callers.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
