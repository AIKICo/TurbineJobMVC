using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection;

namespace TurbineJobMVC.CustomMiddleware
{
    public class CheckBrowserMiddleware
    {
        private readonly string[] CompatibleBrowsers = {"Chrome", "Firefox", "Edge", "Safari"};
        private readonly RequestDelegate _next;

        public CheckBrowserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IDetection detection)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
            {
                if (!CompatibleBrowsers.Contains(detection.Browser.Type.ToString()))
                    context.Response.Redirect("/InCompatibleBrowser.html");
                else
                    await _next.Invoke(context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}