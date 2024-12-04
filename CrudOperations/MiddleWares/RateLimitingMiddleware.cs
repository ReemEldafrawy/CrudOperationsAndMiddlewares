using System.Diagnostics.Metrics;

namespace CrudOperations.MiddleWares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static int counter = 0;
        private static DateTime lastdatetime = DateTime.Now;
        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            ++counter;
            if (DateTime.Now.Subtract(lastdatetime).Seconds > 10)
            {
                counter = 1;
                lastdatetime = DateTime.Now;
            }
            else
            {
                if(counter>5)
                {
                    lastdatetime = DateTime.Now;
                    context.Response.WriteAsync("Rate Limit exceed");

                }
                else
                {
                    lastdatetime = DateTime.Now;
                    await _next(context);
                }
            }
        }
    }
}
