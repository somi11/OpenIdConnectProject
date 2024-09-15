using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime.CompilerServices;

namespace ImageGallery.Client.Middlewares
{
    public static class CookieDeletionMiddlewareExtensions

    {
        public static IApplicationBuilder UseCookieDeletion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CookieDeletionMiddleware>();
        }
    }

    public class CookieDeletionMiddleware { 
        public readonly RequestDelegate _next;

        public CookieDeletionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

       public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey(CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationScheme))
            {
                context.Response.Cookies.Delete(CookieAuthenticationDefaults.CookiePrefix + CookieAuthenticationDefaults.AuthenticationScheme);
            }
            await _next(context);
        }
    }
}
