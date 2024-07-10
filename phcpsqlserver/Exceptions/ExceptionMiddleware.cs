using Microsoft.AspNetCore.Mvc;

namespace Phc.Middleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task<ContentResult> InvokeAsync(HttpContext httpContext)
        {
            try{
                await _next(httpContext);
                return null;
            }
            catch(InvalidOperationException e){
                ContentResult result = new ContentResult();
                result.Content = ":(";
                result.StatusCode = 404;
                return result;
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
    }
}





