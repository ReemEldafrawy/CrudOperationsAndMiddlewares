using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text.Json;

namespace CrudOperations.Filiters
{
    public class SensitiveInformationFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            Debug.WriteLine($"Sensitive Information {context.HttpContext.User} and{JsonSerializer.Serialize(context.ActionArguments)}");
        }
    }
}
