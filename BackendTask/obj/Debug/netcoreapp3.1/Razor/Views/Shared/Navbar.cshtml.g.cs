#pragma checksum "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8dd9891229d4009dec1933337eb2b7d4ed8e545e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Navbar), @"mvc.1.0.view", @"/Views/Shared/Navbar.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\_ViewImports.cshtml"
using BackendTask;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\_ViewImports.cshtml"
using BackendTask.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\_ViewImports.cshtml"
using BackendTask.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\_ViewImports.cshtml"
using System.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8dd9891229d4009dec1933337eb2b7d4ed8e545e", @"/Views/Shared/Navbar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b80786a944b5b2417f4a75f404207bf7a76d7fd", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Navbar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<header>\r\n    <nav class=\"navbar navbar-expand-sm bg-light\">\r\n        <a class=\"navbar-brand\" href=\"#\">BackendTask</a>\r\n        <ul class=\"navbar-nav ml-2\">\r\n");
#nullable restore
#line 5 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml"
             if (User.IsInRole("admin"))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <li class=""nav-item"">
                    <a class=""nav-link"" href=""/Reports/UserReports"">User Reports</a>
                </li>
                <li class=""nav-item ml-2"">
                    <a class=""nav-link"" href=""/Reports/RegistrationReport"">Registration Report</a>
                </li>
                <li class=""nav-item ml-2"">
                    <a class=""nav-link"" href=""/Reports/LoginTimeReport"">Login Report</a>
                </li>
");
#nullable restore
#line 16 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </ul>\r\n        <ul class=\"navbar-nav ml-auto\">\r\n");
#nullable restore
#line 20 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml"
             if (User.Identity.IsAuthenticated)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item\">\r\n                    <a class=\"nav-link\" href=\"/account/logout\">Logout </a>\r\n                </li>\r\n");
#nullable restore
#line 25 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <li class=""nav-item"">
                    <a class=""nav-link"" href=""/account/register"">Sign Up</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""/account/login"">Login</a>
                </li>
");
#nullable restore
#line 34 "C:\Users\ademh\source\repos\BackendTask\BackendTask\Views\Shared\Navbar.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        </ul>\r\n    </nav>\r\n</header>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
