using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcosystemApp.Filters
{
    public class PrivateAttribute : ActionFilterAttribute
    {
        public string? Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = context.HttpContext.Session.GetString("username");
            var userRole = context.HttpContext.Session.GetString("rol");

            if (!string.IsNullOrEmpty(userName))
            {
                if (string.IsNullOrEmpty(Role)) base.OnActionExecuting(context);
                else if (Role.Contains(userRole)) base.OnActionExecuting(context);
                else context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}
