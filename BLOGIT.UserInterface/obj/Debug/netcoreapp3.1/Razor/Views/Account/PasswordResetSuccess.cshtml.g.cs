#pragma checksum "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\Account\PasswordResetSuccess.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54e8b4c7f8daac791abf0354d992816a2b203846"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_PasswordResetSuccess), @"mvc.1.0.view", @"/Views/Account/PasswordResetSuccess.cshtml")]
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
#line 1 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using BLOGIT.UserInterface;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using BLOGIT.UserInterface.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using BLOGIT.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using BLOGIT.Models.UserServicesViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\_ViewImports.cshtml"
using BLOGIT.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54e8b4c7f8daac791abf0354d992816a2b203846", @"/Views/Account/PasswordResetSuccess.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b854a2e74e98bdaf89a24d8bdb3b24b4e7afacf8", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_PasswordResetSuccess : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\hp\source\repos\BLogitMain\BLOGIT.UserInterface\Views\Account\PasswordResetSuccess.cshtml"
   
    ViewBag.Title = "Password Reset success";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"container\" style=\"padding: 7em 0; \">\r\n    <div class=\"row justify-content-center\">\r\n        <div class=\"col text-center\">\r\n            <h1");
            BeginWriteAttribute("class", " class=\"", 209, "\"", 217, 0);
            EndWriteAttribute();
            WriteLiteral("style=\"color: rebeccapurple\">\r\n                    PASSWORD RESET SUCCESSFUL ✨\r\n            </h1>\r\n            <h3 class=\"m-2\">\r\n                WELCOME BACK 🧾🧾🧾\r\n            </h3>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<BlogUser> _signInMngr { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<BlogUser> _userMngr { get; private set; }
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