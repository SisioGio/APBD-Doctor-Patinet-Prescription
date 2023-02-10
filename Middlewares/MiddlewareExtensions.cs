using Microsoft.AspNetCore.Builder;

namespace TASK9.Middlewares
{

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}