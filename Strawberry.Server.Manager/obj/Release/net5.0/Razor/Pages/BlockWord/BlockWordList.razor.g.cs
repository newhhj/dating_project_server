#pragma checksum "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50d71e5898789f4230a2f5aaed8f25370e4bda3d"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.BlockWord
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Strawberry.Server.Manager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\_Imports.razor"
using Strawberry.Server.Manager.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/blockwordlist")]
    public partial class BlockWordList : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 4 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\">\r\n        데이터를 불러오는 중입니다.<br>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 10 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<span class=\"label-title\">문구 목록</span>\r\n    ");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "context-box");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "filter-box");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "style", "flex-grow: 1; text-align: end; padding-right: 10px;");
            __builder.OpenElement(8, "button");
            __builder.AddAttribute(9, "class", "accept");
            __builder.AddAttribute(10, "style", "display: inline-block;");
            __builder.AddAttribute(11, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 17 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                                                OpenDetail

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(12, "신규등록");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\r\n        ");
            __builder.OpenElement(14, "table");
            __builder.AddAttribute(15, "cellpadding", "0");
            __builder.AddAttribute(16, "cellspacing", "0");
            __builder.AddMarkupContent(17, "<thead><tr><th>\r\n                        단어\r\n                    </th>\r\n                    <th style=\"width: 160px;\">\r\n                        -\r\n                    </th></tr></thead>\r\n            ");
            __builder.OpenElement(18, "tbody");
#nullable restore
#line 32 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                 if (this.Items == null || this.Items.Length == 0)
                {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(19, "<tr><td colspan=\"2\"><div class=\"no-list\">\r\n                                표시할 목록이 없습니다.\r\n                            </div></td></tr>");
#nullable restore
#line 41 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                }
                else
                {
                    foreach (var item in this.Items)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(20, "tr");
            __builder.OpenElement(21, "td");
            __builder.OpenElement(22, "input");
            __builder.AddAttribute(23, "style", "width: 100%;");
            __builder.AddAttribute(24, "placeholder", "단어를 입력하세요.");
            __builder.AddAttribute(25, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 48 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                                   item.Word

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(26, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => item.Word = __value, item.Word));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n                            ");
            __builder.OpenElement(28, "td");
            __builder.OpenElement(29, "button");
            __builder.AddAttribute(30, "style", "background-color: #4a9cff; color: #ffffff;");
            __builder.AddAttribute(31, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 51 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                                                                     async () => { await this.AcceptItemAsync(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(32, "적용");
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n                                ");
            __builder.OpenElement(34, "button");
            __builder.AddAttribute(35, "style", "background-color: #e11b1b; color: #ffffff;");
            __builder.AddAttribute(36, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 52 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                                                                     async () => { await this.RemoveItemAsync(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(37, "삭제");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 55 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                    }
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n            ");
            __builder.OpenElement(39, "tfoot");
            __builder.OpenElement(40, "tr");
            __builder.OpenElement(41, "td");
            __builder.AddAttribute(42, "colspan", "2");
            __builder.OpenElement(43, "div");
            __builder.AddAttribute(44, "class", "pager");
            __builder.OpenElement(45, "btn");
            __builder.AddAttribute(46, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 62 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                           () => { this.Page--; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(47, "<i class=\"fas fa-chevron-left\"></i>");
            __builder.CloseElement();
            __builder.AddMarkupContent(48, "\r\n                            ");
            __builder.OpenElement(49, "input");
            __builder.AddAttribute(50, "type", "number");
            __builder.AddAttribute(51, "onkeydown", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.KeyboardEventArgs>(this, 
#nullable restore
#line 65 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                                                                     (e) => { if (e.Key == "Enter") this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(52, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 65 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                                        this.Page

#line default
#line hidden
#nullable disable
            , culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.AddAttribute(53, "oninput", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.Page = __value, this.Page, culture: global::System.Globalization.CultureInfo.InvariantCulture));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.AddMarkupContent(54, "\r\n                            ");
            __builder.OpenElement(55, "span");
            __builder.AddContent(56, "/ ");
#nullable restore
#line 66 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
__builder.AddContent(57, this.TotalPageCount.ToString("#,##0"));

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(58, "\r\n                            ");
            __builder.OpenElement(59, "btn");
            __builder.AddAttribute(60, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 67 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
                                           () => { this.Page++; this.MovePage(); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(61, "<i class=\"fas fa-chevron-right\"></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 76 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\BlockWord\BlockWordList.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
