#pragma checksum "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Index\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "56d5d631f7f16633c07c4367e26149a8bef0c5b7"
// <auto-generated/>
#pragma warning disable 1591
namespace Strawberry.Server.Manager.Pages.Index
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
#line 2 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Index\Index.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "D:\외주\만두딸기\2022.11.13. 제공하 소스파일 일체\1번째 개발자\Strawberry 2022.03.17 (2)\Strawberry.Server.Manager\Pages\Index\Index.razor"
       
	protected override void OnAfterRender(bool firstRender)
	{
		if (firstRender)
		{
			this.Navi.NavigateTo("/setting");
		}
	}

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager Navi { get; set; }
    }
}
#pragma warning restore 1591
