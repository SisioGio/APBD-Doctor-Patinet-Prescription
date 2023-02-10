using System.Threading.Tasks;

namespace TASK9.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {



                using (StreamWriter w = File.AppendText("./log.txt"))
                {
                    w.WriteLine(
                                        "{0} {1} - {2}",
                                        DateTime.Now.ToLongTimeString(),
                                        DateTime.Now.ToLongDateString(),
                                        ex.Message
                                        );
                }

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync($"Unexpected error: {ex}");
            }






        }

    }
}