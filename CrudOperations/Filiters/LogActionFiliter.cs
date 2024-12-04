using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Abstractions;
using System.Text.Json;

namespace CrudOperations.Filiters
{
    public class LogActionFiliter : IActionFilter,IAsyncActionFilter
    {
        private readonly ILogger<LogActionFiliter> _logger;
        public LogActionFiliter(ILogger<LogActionFiliter>logger)
        { 
            _logger = logger;
        }
        //Action Filter Before
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"executing action {context.ActionDescriptor.DisplayName} and the controller is {context.Controller} and the araguments {JsonSerializer.Serialize( context.ActionArguments)}");
            
        }
        //Action Filter after
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"excuteed action {context.ActionDescriptor.DisplayName} and the controller (Finished execution) {context.Controller}");
            context.Result = new NotFoundResult();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"(Async) executing action {context.ActionDescriptor.DisplayName} and the controller is {context.Controller} and the araguments {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            _logger.LogInformation($"(Async)excuteed action {context.ActionDescriptor.DisplayName} and the controller (Finished execution) {context.Controller}");
           
        }
    }
}
