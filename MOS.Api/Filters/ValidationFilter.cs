using Microsoft.AspNetCore.Mvc.Filters;

namespace MOS.Api.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // TODO: check ModelState.IsValid
            // TODO: if invalid return 400 with validation errors
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // nothing needed here
        }
    }
}
