#pragma checksum "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57c3b31e78fc52fd7637609f38bbdb5868f54412"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.AD
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
#nullable restore
#line 2 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Components.LayoutAttribute(typeof(EmptyLayout))]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/addetail/{id:int?}")]
    public partial class ADDetail : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 5 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\" b-9g78uvet85>\r\n        데이터를 불러오는 중입니다.<br b-9g78uvet85>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 11 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "style", "padding: 10px;");
            __builder.AddAttribute(3, "b-9g78uvet85");
            __builder.AddMarkupContent(4, "<span class=\"label-title\" b-9g78uvet85>광고 정보</span>\r\n        ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "context-box");
            __builder.AddAttribute(7, "b-9g78uvet85");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "row-box");
            __builder.AddAttribute(10, "b-9g78uvet85");
            __builder.AddMarkupContent(11, "<span class=\"label-name\" b-9g78uvet85>광고명</span>\r\n                ");
            __builder.OpenElement(12, "input");
            __builder.AddAttribute(13, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 19 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                              this.PageItem.ADName

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.ADName = __value, this.PageItem.ADName));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(15, "b-9g78uvet85");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n            ");
            __builder.OpenElement(17, "div");
            __builder.AddAttribute(18, "class", "row-box");
            __builder.AddAttribute(19, "b-9g78uvet85");
            __builder.AddMarkupContent(20, "<span class=\"label-name\" b-9g78uvet85>광고 타입</span>\r\n                ");
            __builder.OpenElement(21, "select");
            __builder.AddAttribute(22, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 23 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                               this.PageItem.ADType

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(23, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.ADType = __value, this.PageItem.ADType));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(24, "b-9g78uvet85");
            __builder.OpenElement(25, "option");
            __builder.AddAttribute(26, "value", 
#nullable restore
#line 24 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                    Database.ADTypes.MainPopup

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(27, "b-9g78uvet85");
            __builder.AddMarkupContent(28, "메인팝업");
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n                    ");
            __builder.OpenElement(30, "option");
            __builder.AddAttribute(31, "value", 
#nullable restore
#line 25 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                    Database.ADTypes.SettingBanner

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(32, "b-9g78uvet85");
            __builder.AddMarkupContent(33, "설정배너");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n            ");
            __builder.OpenElement(35, "div");
            __builder.AddAttribute(36, "class", "row-box");
            __builder.AddAttribute(37, "b-9g78uvet85");
            __builder.AddMarkupContent(38, "<span class=\"label-name\" b-9g78uvet85>이미지</span>\r\n                ");
            __builder.OpenElement(39, "div");
            __builder.AddAttribute(40, "class", "img-box");
            __builder.AddAttribute(41, "b-9g78uvet85");
            __builder.OpenElement(42, "div");
            __builder.AddAttribute(43, "b-9g78uvet85");
#nullable restore
#line 32 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                         if (!string.IsNullOrWhiteSpace(this.PageItem.JsonData))
                        {
                            var data = JsonConvert.DeserializeObject<dynamic>(this.PageItem.JsonData);

                            switch (this.PageItem.ADType)
                            {
                                case Database.ADTypes.MainPopup:
                                case Database.ADTypes.SettingBanner:

#line default
#line hidden
#nullable disable
            __builder.OpenElement(44, "div");
            __builder.AddAttribute(45, "b-9g78uvet85");
            __builder.OpenElement(46, "div");
            __builder.AddAttribute(47, "class", "btn-removeimg");
            __builder.AddAttribute(48, "b-9g78uvet85");
            __builder.OpenElement(49, "button");
            __builder.AddAttribute(50, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 42 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                                              this.ImageRemove

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(51, "b-9g78uvet85");
            __builder.AddMarkupContent(52, "<i class=\"fas fa-times\" b-9g78uvet85></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n                                        ");
            __builder.OpenElement(54, "img");
            __builder.AddAttribute(55, "style", "cursor: pointer;");
            __builder.AddAttribute(56, "src", 
#nullable restore
#line 46 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                                                            data.ImageUrl

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(57, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 46 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                                                                                     () => { this.OpenWindowDetailImage(data.ImageUrl); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(58, "b-9g78uvet85");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 48 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                    break;
                            }
                        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(59, "\r\n                ");
            __builder.AddMarkupContent(60, "<button class=\"accept\" style=\"width: 100%; margin-top: 5px;\" onclick=\"$(this).next().click();\" b-9g78uvet85>+</button>\r\n                ");
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Forms.InputFile>(61);
            __builder.AddAttribute(62, "style", "display: none;");
            __builder.AddAttribute(63, "OnChange", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.EventCallback<global::Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>>(global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>(this, 
#nullable restore
#line 54 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                                            ImageUpload

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(64, "\r\n            ");
            __builder.OpenElement(65, "div");
            __builder.AddAttribute(66, "class", "row-box");
            __builder.AddAttribute(67, "b-9g78uvet85");
            __builder.AddMarkupContent(68, "<span class=\"label-name\" b-9g78uvet85>연결링크</span>\r\n                ");
            __builder.OpenElement(69, "input");
            __builder.AddAttribute(70, "placeholder", "http://");
            __builder.AddAttribute(71, "value", global::Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 58 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                              this.PageItem.Link

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(72, "onchange", global::Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Link = __value, this.PageItem.Link));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(73, "b-9g78uvet85");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(74, "\r\n        ");
            __builder.OpenElement(75, "button");
            __builder.AddAttribute(76, "class", "accept");
            __builder.AddAttribute(77, "style", "width: 100%; margin-top: 10px;");
            __builder.AddAttribute(78, "onclick", global::Microsoft.AspNetCore.Components.EventCallback.Factory.Create<global::Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 61 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
                                                                                AcceptMemberAsync

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(79, "b-9g78uvet85");
            __builder.AddMarkupContent(80, "적용");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 63 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\AD\ADDetail.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591