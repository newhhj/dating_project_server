#pragma checksum "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\App.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7312fd7984796c5592299d3ce49b44fad96f0f8d"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager
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
    public partial class App : global::Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<global::Microsoft.AspNetCore.Components.Routing.Router>(0);
            __builder.AddAttribute(1, "AppAssembly", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Reflection.Assembly>(
#nullable restore
#line 1 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\App.razor"
                      typeof(Program).Assembly

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(2, "PreferExactMatches", 
#nullable restore
#line 1 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\App.razor"
                                                                     true

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(3, "Found", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.RouteData>)((routeData) => (__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.Authorization.AuthorizeRouteView>(4);
                __builder2.AddAttribute(5, "RouteData", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::Microsoft.AspNetCore.Components.RouteData>(
#nullable restore
#line 3 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\App.razor"
                                        routeData

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(6, "NotAuthorized", (global::Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder3) => {
                    __builder3.OpenComponent<global::Microsoft.AspNetCore.Components.LayoutView>(7);
                    __builder3.AddAttribute(8, "Layout", global::Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Type>(
#nullable restore
#line 5 "D:\1.SourceXamarin\Strawberry\Strawberry.Server.Manager\App.razor"
                                     typeof(LoginLayout)

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(9, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder4) => {
                        __builder4.OpenComponent<global::Strawberry.Server.Manager.Pages.Login.Login>(10);
                        __builder4.CloseComponent();
                    }
                    ));
                    __builder3.CloseComponent();
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.AddAttribute(11, "NotFound", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenComponent<global::Microsoft.AspNetCore.Components.LayoutView>(12);
                __builder2.AddAttribute(13, "ChildContent", (global::Microsoft.AspNetCore.Components.RenderFragment)((__builder3) => {
                    __builder3.AddMarkupContent(14, "<p>Sorry, there\'s nothing at this address.</p>");
                }
                ));
                __builder2.CloseComponent();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
