using System.Diagnostics;

namespace CrudOperations.MiddleWares
{
    public class ProfilingMaddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ProfilingMaddleware> _Logger;
        public ProfilingMaddleware(RequestDelegate Next, ILogger<ProfilingMaddleware> logger)
        {
            _Next = Next;
            _Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await _Next(context);
            stopwatch.Stop();
            _Logger.LogInformation($"Request {context.Request.Path} took {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
