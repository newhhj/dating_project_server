#pragma checksum "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b00937e4f5525ddecbce85991fa73ef21285cc90"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.Alert
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
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/alertlist")]
    public partial class AlertList : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 4 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\" b-1cdc8ouq5t>\r\n        데이터를 불러오는 중입니다.<br b-1cdc8ouq5t>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 10 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<span class=\"label-title\" b-1cdc8ouq5t>신고 목록</span>\r\n    ");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "context-box");
            __builder.AddAttribute(4, "b-1cdc8ouq5t");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "filter-box");
            __builder.AddAttribute(7, "b-1cdc8ouq5t");
            __builder.OpenElement(8, "input");
            __builder.AddAttribute(9, "name", "contentType");
            __builder.AddAttribute(10, "type", "radio");
            __builder.AddAttribute(11, "id", "radio01");
            __builder.AddAttribute(12, "value", "0");
            __builder.AddAttribute(13, "checked", 
#nullable restore
#line 16 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                     this.ContentType == 0

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(14, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 16 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                                                       () => { this.ChangeContentType(0); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "b-1cdc8ouq5t");
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "<label for=\"radio01\" b-1cdc8ouq5t>프로필</label>\r\n            ");
            __builder.OpenElement(17, "input");
            __builder.AddAttribute(18, "name", "contentType");
            __builder.AddAttribute(19, "type", "radio");
            __builder.AddAttribute(20, "id", "radio02");
            __builder.AddAttribute(21, "value", "1");
            __builder.AddAttribute(22, "checked", 
#nullable restore
#line 17 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                     this.ContentType == 1

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(23, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 17 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                                                       () => { this.ChangeContentType(1); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "b-1cdc8ouq5t");
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "<label for=\"radio02\" b-1cdc8ouq5t>뿜뿜</label>\r\n            ");
            __builder.OpenElement(26, "input");
            __builder.AddAttribute(27, "name", "contentType");
            __builder.AddAttribute(28, "type", "radio");
            __builder.AddAttribute(29, "id", "radio03");
            __builder.AddAttribute(30, "value", "2");
            __builder.AddAttribute(31, "checked", 
#nullable restore
#line 18 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                     this.ContentType == 2

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(32, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 18 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                                                       () => { this.ChangeContentType(2); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(33, "b-1cdc8ouq5t");
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "<label for=\"radio03\" b-1cdc8ouq5t>댓글</label>");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n        ");
            __builder.OpenElement(36, "table");
            __builder.AddAttribute(37, "cellpadding", "0");
            __builder.AddAttribute(38, "cellspacing", "0");
            __builder.AddAttribute(39, "b-1cdc8ouq5t");
            __builder.AddMarkupContent(40, @"<thead b-1cdc8ouq5t><tr b-1cdc8ouq5t><th b-1cdc8ouq5t>
                        제목
                    </th>
                    <th style=""width: 200px;"" b-1cdc8ouq5t>
                        등록시간
                    </th>
                    <th style=""width: 160px;"" b-1cdc8ouq5t>
                        -
                    </th></tr></thead>
            ");
            __builder.OpenElement(41, "tbody");
            __builder.AddAttribute(42, "b-1cdc8ouq5t");
#nullable restore
#line 35 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                 if (this.Items == null || this.Items.Length == 0)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(43, "<tr b-1cdc8ouq5t><td colspan=\"3\" b-1cdc8ouq5t><div class=\"no-list\" b-1cdc8ouq5t>\r\n                                표시할 목록이 없습니다.\r\n                            </div></td></tr>");
#nullable restore
#line 44 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                }
                else
                {
                    foreach (var item in this.Items)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(44, "tr");
            __builder.AddAttribute(45, "b-1cdc8ouq5t");
            __builder.OpenElement(46, "td");
            __builder.AddAttribute(47, "b-1cdc8ouq5t");
            __builder.OpenElement(48, "span");
            __builder.AddAttribute(49, "style", "font-weight: bold; cursor: pointer;");
            __builder.AddAttribute(50, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 51 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                            () => { this.OpenDetail(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(51, "b-1cdc8ouq5t");
            __builder.AddContent(52, 
#nullable restore
#line 52 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                     item.Title

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n                            ");
            __builder.OpenElement(54, "td");
            __builder.AddAttribute(55, "b-1cdc8ouq5t");
            __builder.AddContent(56, 
#nullable restore
#line 56 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                 item.CreateTime.ToString("yyyy.MM.dd")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(57, "\r\n                            ");
            __builder.OpenElement(58, "td");
            __builder.AddAttribute(59, "b-1cdc8ouq5t");
            __builder.OpenElement(60, "button");
            __builder.AddAttribute(61, "style", "background-color: #e11b1b; color: #ffffff;");
            __builder.AddAttribute(62, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 59 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                                     async () => { await this.RemoveItemAsync(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(63, "b-1cdc8ouq5t");
            __builder.AddMarkupContent(64, "삭제");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 62 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(65, "\r\n            ");
            __builder.OpenElement(66, "tfoot");
            __builder.AddAttribute(67, "b-1cdc8ouq5t");
            __builder.OpenElement(68, "tr");
            __builder.AddAttribute(69, "b-1cdc8ouq5t");
            __builder.OpenElement(70, "td");
            __builder.AddAttribute(71, "colspan", "3");
            __builder.AddAttribute(72, "b-1cdc8ouq5t");
            __builder.OpenElement(73, "div");
            __builder.AddAttribute(74, "class", "pager");
            __builder.AddAttribute(75, "b-1cdc8ouq5t");
            __builder.OpenElement(76, "btn");
            __builder.AddAttribute(77, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 69 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                           () => { this.Page--; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(78, "b-1cdc8ouq5t");
            __builder.AddMarkupContent(79, "<i class=\"fas fa-chevron-left\" b-1cdc8ouq5t></i>");
            __builder.CloseElement();
            __builder.AddMarkupContent(80, "\r\n                            ");
            __builder.OpenElement(81, "input");
            __builder.AddAttribute(82, "type", "number");
            __builder.AddAttribute(83, "onkeydown", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 72 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                                                                     (e) => { if (e.Key == "Enter") this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(84, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 72 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                                        this.Page

#line default
#line hidden
#nullable disable
            , culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(85, "oninput", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.Page = __value, this.Page, culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(86, "b-1cdc8ouq5t");
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n                            ");
            __builder.OpenElement(88, "span");
            __builder.AddAttribute(89, "b-1cdc8ouq5t");
            __builder.AddContent(90, "/ ");
            __builder.AddContent(91, 
#nullable restore
#line 73 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                     this.TotalPageCount.ToString("#,##0")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(92, "\r\n                            ");
            __builder.OpenElement(93, "btn");
            __builder.AddAttribute(94, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 74 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
                                           () => { this.Page++; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(95, "b-1cdc8ouq5t");
            __builder.AddMarkupContent(96, "<i class=\"fas fa-chevron-right\" b-1cdc8ouq5t></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 83 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Alert\AlertList.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591