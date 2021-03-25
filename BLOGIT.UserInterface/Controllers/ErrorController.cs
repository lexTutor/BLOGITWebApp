using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BLOGIT.UserInterface.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> Logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            Logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 404)
            {
                IStatusCodeReExecuteFeature statusDetail = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

                if (statusDetail.OriginalQueryString == null)
                    Logger.LogError($"Message: Error 404! Page not found., Error-Path: {statusDetail.OriginalPath}");
                else
                    Logger.LogError($"Message: Error 404! Page not found., Error-Path: {statusDetail.OriginalPath}, " +
                        $"Query String: {statusDetail.OriginalQueryString}");
            }
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var statusDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            Logger.LogError($"Error-Path: {statusDetail.Path}, Error Message: {statusDetail.Error}");

            return View("NoResponse");
        }
    }
}
