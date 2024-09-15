namespace ImageGallery.Client.Middlewares
{
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ContentSecurityPolicyMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers["Content-Security-Policy"] = "default-src *; script-src * 'unsafe-inline' 'unsafe-eval'; connect-src *; img-src *; style-src * 'unsafe-inline';";
            await _next(context);
        }

    }

}
