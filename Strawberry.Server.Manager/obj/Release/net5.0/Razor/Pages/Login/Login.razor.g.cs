#pragma checksum "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\Pages\Login\Login.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cba2d8bc6120313845e303c6d695c86be4e98791"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.Login
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
    [global::Microsoft.AspNetCore.Components.LayoutAttribute(typeof(LoginLayout))]
    [global::Microsoft.AspNetCore.Components.RouteAttribute("/login")]
    public partial class Login : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, @"<div class=""login-pannel"" b-l1qhkzgrjk><div class=""login-box"" b-l1qhkzgrjk><h2 b-l1qhkzgrjk>관리자 로그인</h2>
        <span b-l1qhkzgrjk>아이디</span>
        <input id=""userid"" placeholder=""아이디를 입력하세요."" onkeydown=""checkKeyDown();"" b-l1qhkzgrjk>
        <span b-l1qhkzgrjk>비밀번호</span>
        <input id=""passwd"" type=""password"" placeholder=""비밀번호를 입력하세요."" onkeydown=""checkKeyDown();"" b-l1qhkzgrjk>
        <button onclick=""login();"" b-l1qhkzgrjk>로그인</button></div></div>");
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
