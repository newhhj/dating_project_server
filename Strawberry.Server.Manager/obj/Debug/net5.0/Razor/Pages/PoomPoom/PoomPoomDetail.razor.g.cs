#pragma checksum "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c9ecabd9c923925cfdf146d6273838c3a32288a"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.PoomPoom
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
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(EmptyLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/poompoomdetail/{id:int?}")]
    public partial class PoomPoomDetail : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 4 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
 if (!this.IsInit)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"pageloading-box\" b-uxrbizmvob>\r\n        데이터를 불러오는 중입니다.<br b-uxrbizmvob>\r\n        잠시만 기다려 주세요.\r\n    </div>");
#nullable restore
#line 10 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "style", "padding: 10px;");
            __builder.AddAttribute(3, "b-uxrbizmvob");
            __builder.AddMarkupContent(4, "<span class=\"label-title\" b-uxrbizmvob>컨텐츠 정보</span>\r\n        ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "context-box");
            __builder.AddAttribute(7, "b-uxrbizmvob");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "row-box");
            __builder.AddAttribute(10, "b-uxrbizmvob");
            __builder.AddMarkupContent(11, "<span class=\"label-name\" b-uxrbizmvob>닉네임</span>");
#nullable restore
#line 18 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                 if (this.Id == 0)
                {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(12, "select");
            __builder.AddAttribute(13, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 20 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                   this.PageItem.Item.MemberId

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(14, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.MemberId = __value, this.PageItem.Item.MemberId));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(15, "b-uxrbizmvob");
            __builder.OpenElement(16, "option");
            __builder.AddAttribute(17, "value", "0");
            __builder.AddAttribute(18, "b-uxrbizmvob");
            __builder.AddMarkupContent(19, "== 회원을 선택하세요 ==");
            __builder.CloseElement();
#nullable restore
#line 22 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                         foreach (var memberitem in this.MemberList)
                        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(20, "option");
            __builder.AddAttribute(21, "value", 
#nullable restore
#line 24 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                            memberitem.Id

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(22, "b-uxrbizmvob");
            __builder.AddContent(23, 
#nullable restore
#line 24 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                            memberitem.Nickname

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 25 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 27 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                }
                else
                {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(24, "input");
            __builder.AddAttribute(25, "disabled");
            __builder.AddAttribute(26, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 30 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                  this.PageItem.Nickname

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(27, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Nickname = __value, this.PageItem.Nickname));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(28, "b-uxrbizmvob");
            __builder.CloseElement();
#nullable restore
#line 31 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(29, "\r\n            ");
            __builder.OpenElement(30, "div");
            __builder.AddAttribute(31, "class", "row-box");
            __builder.AddAttribute(32, "b-uxrbizmvob");
            __builder.AddMarkupContent(33, "<span class=\"label-name\" b-uxrbizmvob>컨텐츠 타입</span>\r\n                ");
            __builder.OpenElement(34, "select");
            __builder.AddAttribute(35, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 35 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                               this.PageItem.Item.ContentType

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(36, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.ContentType = __value, this.PageItem.Item.ContentType));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(37, "b-uxrbizmvob");
            __builder.OpenElement(38, "option");
            __builder.AddAttribute(39, "value", 
#nullable restore
#line 36 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                    Database.ContentTypes.Boast

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(40, "b-uxrbizmvob");
            __builder.AddMarkupContent(41, "자랑");
            __builder.CloseElement();
            __builder.AddMarkupContent(42, "\r\n                    ");
            __builder.OpenElement(43, "option");
            __builder.AddAttribute(44, "value", 
#nullable restore
#line 37 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                    Database.ContentTypes.Metting

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(45, "b-uxrbizmvob");
            __builder.AddMarkupContent(46, "번개");
            __builder.CloseElement();
            __builder.AddMarkupContent(47, "\r\n                    ");
            __builder.OpenElement(48, "option");
            __builder.AddAttribute(49, "value", 
#nullable restore
#line 38 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                    Database.ContentTypes.Sell

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(50, "b-uxrbizmvob");
            __builder.AddMarkupContent(51, "판매");
            __builder.CloseElement();
            __builder.AddMarkupContent(52, "\r\n                    ");
            __builder.OpenElement(53, "option");
            __builder.AddAttribute(54, "value", 
#nullable restore
#line 39 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                    Database.ContentTypes.Talk

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(55, "b-uxrbizmvob");
            __builder.AddMarkupContent(56, "수다");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(57, "\r\n            ");
            __builder.OpenElement(58, "div");
            __builder.AddAttribute(59, "class", "row-box");
            __builder.AddAttribute(60, "b-uxrbizmvob");
            __builder.AddMarkupContent(61, "<span class=\"label-name\" b-uxrbizmvob>이미지</span>\r\n                ");
            __builder.OpenElement(62, "div");
            __builder.AddAttribute(63, "class", "img-box");
            __builder.AddAttribute(64, "b-uxrbizmvob");
            __builder.OpenElement(65, "div");
            __builder.AddAttribute(66, "b-uxrbizmvob");
#nullable restore
#line 46 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                         if (this.PageItem.Item.PoomPoom_Images != null)
                        {
                            foreach (var item in this.PageItem.Item.PoomPoom_Images)
                            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(67, "div");
            __builder.AddAttribute(68, "b-uxrbizmvob");
            __builder.OpenElement(69, "div");
            __builder.AddAttribute(70, "class", "btn-removeimg");
            __builder.AddAttribute(71, "b-uxrbizmvob");
            __builder.OpenElement(72, "button");
            __builder.AddAttribute(73, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 52 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                          () => { this.RemoveProfileImage(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(74, "b-uxrbizmvob");
            __builder.AddMarkupContent(75, "<i class=\"fas fa-times\" b-uxrbizmvob></i>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(76, "\r\n                                    ");
            __builder.OpenElement(77, "img");
            __builder.AddAttribute(78, "style", "cursor: pointer;");
            __builder.AddAttribute(79, "src", 
#nullable restore
#line 56 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                                        item.Url

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(80, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 56 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                                                            () => { this.OpenWindowDetailImage(item); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(81, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 58 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                            }
                        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(82, "\r\n                ");
            __builder.AddMarkupContent(83, "<button class=\"accept\" style=\"width: 100%; margin-top: 5px;\" onclick=\"$(this).next().click();\" b-uxrbizmvob>+</button>\r\n                ");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputFile>(84);
            __builder.AddAttribute(85, "style", "display: none;");
            __builder.AddAttribute(86, "OnChange", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.InputFileChangeEventArgs>(this, 
#nullable restore
#line 63 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                            ImageUpload

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(87, "\r\n            ");
            __builder.OpenElement(88, "div");
            __builder.AddAttribute(89, "class", "row-box");
            __builder.AddAttribute(90, "b-uxrbizmvob");
            __builder.AddMarkupContent(91, "<span class=\"label-name\" b-uxrbizmvob>내용</span>\r\n                ");
            __builder.OpenElement(92, "textarea");
            __builder.AddAttribute(93, "style", "height: 160px;");
            __builder.AddAttribute(94, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 67 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                 this.PageItem.Item.Content

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(95, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.Content = __value, this.PageItem.Item.Content));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(96, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(97, "\r\n            ");
            __builder.OpenElement(98, "div");
            __builder.AddAttribute(99, "class", "row-box");
            __builder.AddAttribute(100, "b-uxrbizmvob");
            __builder.AddMarkupContent(101, "<span class=\"label-name\" b-uxrbizmvob>장소</span>\r\n                ");
            __builder.OpenElement(102, "input");
            __builder.AddAttribute(103, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 71 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                              this.PageItem.Item.Area

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(104, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.Area = __value, this.PageItem.Item.Area));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(105, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(106, "\r\n            ");
            __builder.OpenElement(107, "div");
            __builder.AddAttribute(108, "class", "row-box");
            __builder.AddAttribute(109, "b-uxrbizmvob");
            __builder.AddMarkupContent(110, "<span class=\"label-name\" b-uxrbizmvob>시간</span>\r\n                ");
            __builder.OpenElement(111, "input");
            __builder.AddAttribute(112, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 75 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                              this.PageItem.Item.Time

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(113, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.Time = __value, this.PageItem.Item.Time));
            __builder.SetUpdatesAttributeName("value");
            __builder.AddAttribute(114, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(115, "\r\n            ");
            __builder.OpenElement(116, "div");
            __builder.AddAttribute(117, "class", "row-box");
            __builder.AddAttribute(118, "b-uxrbizmvob");
            __builder.AddMarkupContent(119, "<span class=\"label-name\" b-uxrbizmvob>댓글 허용여부</span>\r\n                ");
            __builder.OpenElement(120, "input");
            __builder.AddAttribute(121, "type", "checkbox");
            __builder.AddAttribute(122, "style", "width: 20px; height: 20px; cursor: pointer;");
            __builder.AddAttribute(123, "checked", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 79 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                              this.PageItem.Item.UseComment

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(124, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => this.PageItem.Item.UseComment = __value, this.PageItem.Item.UseComment));
            __builder.SetUpdatesAttributeName("checked");
            __builder.AddAttribute(125, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.AddMarkupContent(126, " 댓글 작성을 허용함\r\n            ");
            __builder.CloseElement();
#nullable restore
#line 82 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
             if (this.PageItem.Item.PoomPoom_Comments != null && this.PageItem.Item.PoomPoom_Comments.Count > 0)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(127, "div");
            __builder.AddAttribute(128, "class", "row-box");
            __builder.AddAttribute(129, "b-uxrbizmvob");
            __builder.AddMarkupContent(130, "<span class=\"label-name\" b-uxrbizmvob>댓글</span>");
#nullable restore
#line 86 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                     foreach (var comment in this.PageItem.Item.PoomPoom_Comments)
                    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(131, "div");
            __builder.AddAttribute(132, "class", "comment-box");
            __builder.AddAttribute(133, "b-uxrbizmvob");
            __builder.OpenElement(134, "i");
            __builder.AddAttribute(135, "class", "fas fa-times");
            __builder.AddAttribute(136, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 89 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                              () => { this.RemoveComment(comment); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(137, "b-uxrbizmvob");
            __builder.CloseElement();
            __builder.AddMarkupContent(138, "\r\n                            ");
            __builder.OpenElement(139, "span");
            __builder.AddAttribute(140, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 90 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                            () => { this.OpenCommentMember(comment.MemberId); }

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(141, "b-uxrbizmvob");
            __builder.AddContent(142, 
#nullable restore
#line 90 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                                                                   comment.Member?.Nickname ?? "-"

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(143, "\r\n                            ");
            __builder.OpenElement(144, "span");
            __builder.AddAttribute(145, "b-uxrbizmvob");
            __builder.AddContent(146, 
#nullable restore
#line 91 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                   comment.Comment

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 93 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
#nullable restore
#line 95 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(147, "\r\n        ");
            __builder.OpenElement(148, "button");
            __builder.AddAttribute(149, "class", "accept");
            __builder.AddAttribute(150, "style", "width: 100%; margin-top: 10px;");
            __builder.AddAttribute(151, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 97 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
                                                                                AcceptMemberAsync

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(152, "b-uxrbizmvob");
            __builder.AddMarkupContent(153, "적용");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 99 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\PoomPoom\PoomPoomDetail.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
